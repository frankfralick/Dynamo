using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;

using Dynamo.Controls;
using DynamoInventor.Properties;
using InventorServices.Persistence;


namespace DynamoInventor
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement.
    /// </summary>
    [GuidAttribute("476f38a1-75f3-450b-a75a-6f030bf012a8")]
    public class DynamoInventor : Inventor.ApplicationAddInServer
    {
        #region Private fields
        private Inventor.Application inventorApplication;
        private DynamoInventorAddinButton dynamoAddinButton;
        private UserInterfaceEvents userInterfaceEvents;
        UserInterfaceManager userInterfaceManager;
        RibbonPanel dynamoRibbonPanel;

        private string commandBarInternalName = "Dynamo:InventorDynamo:DynamoCommandBar";
        private string commandBarDisplayName = "Dynamo";
        private string ribbonPanelInternalName = "Dynamo:InventorDynamo:DynamoRibbonPanel";
        private string ribbonPanelDisplayName = "Dynamo";
        private string buttonInternalName = "Dynamo:InventorDynamo:DynamoButton";
        private string buttonDisplayName = "Dynamo";
        private string commandCategoryInternalName = "Dynamo:InventorDynamo:DynamoCommandCat";
        private string commandCategoryDisplayName = "Dynamo";

        private Inventor.UserInterfaceEventsSink_OnResetCommandBarsEventHandler UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetEnvironmentsEventHandler UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

        private Inventor.ApplicationEvents appEvents = null;
        #endregion

        #region Public constructors
        public DynamoInventor()
        {
        }
        #endregion

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // The FirstTime flag indicates if the addin is loaded for the first time.
            try
            {
                inventorApplication = addInSiteObject.Application;
                InventorPersistenceManager.InventorApplication = inventorApplication;
                userInterfaceManager = inventorApplication.UserInterfaceManager;

                //initialize event delegates
                userInterfaceEvents = inventorApplication.UserInterfaceManager.UserInterfaceEvents;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
                userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
                userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
                userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

                appEvents = inventorApplication.ApplicationEvents;
                appEvents.OnActivateDocument += appEvents_OnActivateDocument;
                appEvents.OnDeactivateDocument += appEvents_OnDeactivateDocument;

                Icon dynamoIcon = Resources.logo_square_32x32;

                //retrieve the GUID for this class
                GuidAttribute addInCLSID;
                addInCLSID = (GuidAttribute)GuidAttribute.GetCustomAttribute(this.GetType(), typeof(GuidAttribute));
                string addInCLSIDString;
                addInCLSIDString = "{" + addInCLSID.Value + "}";

                dynamoAddinButton = new DynamoInventorAddinButton(
                        buttonDisplayName, buttonInternalName, CommandTypesEnum.kShapeEditCmdType,
                        addInCLSIDString, "Initialize Dynamo.",
                        "Dynamo is a visual programming environment for Inventor.", dynamoIcon, dynamoIcon, ButtonDisplayEnum.kDisplayTextInLearningMode);

                CommandCategory assemblyUtilitiesCategory = inventorApplication.CommandManager.CommandCategories.Add(commandCategoryDisplayName, commandCategoryInternalName, addInCLSID);
                assemblyUtilitiesCategory.Add(dynamoAddinButton.ButtonDefinition);

                if (firstTime == true)
                {
                    InterfaceStyleEnum interfaceStyle;
                    interfaceStyle = userInterfaceManager.InterfaceStyle;

                    if (interfaceStyle == InterfaceStyleEnum.kClassicInterface)
                    {
                        CommandBar assemblyUtilityCommandBar;

                        assemblyUtilityCommandBar = userInterfaceManager.CommandBars.Add(commandBarDisplayName,
                                                                                         commandBarInternalName,
                                                                                         CommandBarTypeEnum.kRegularCommandBar,
                                                                                         addInCLSID);
                    }

                    else
                    {   
                        Inventor.Ribbons ribbons = userInterfaceManager.Ribbons;
                        Inventor.Ribbon assemblyRibbon = ribbons["Assembly"];
                        RibbonTabs ribbonTabs = assemblyRibbon.RibbonTabs;
                        RibbonTab assemblyRibbonTab = ribbonTabs["id_TabAssemble"];
                        RibbonPanels ribbonPanels = assemblyRibbonTab.RibbonPanels;
                        dynamoRibbonPanel = ribbonPanels.Add(ribbonPanelDisplayName, ribbonPanelInternalName, "{DB59D9A7-EE4C-434A-BB5A-F93E8866E872}", "", false);
                        CommandControls assemblyRibbonPanelCtrls = dynamoRibbonPanel.CommandControls;
                        CommandControl copyUtilCmdBtnCmdCtrl = assemblyRibbonPanelCtrls.AddButton(dynamoAddinButton.ButtonDefinition, true, true, "", false);              
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void appEvents_OnDeactivateDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventNotHandled;
            if (InventorPersistenceManager.ActiveAssemblyDoc != null)
            {
                //TODO DocumentManager needs to implement Dispose.
                InventorPersistenceManager.ActiveAssemblyDoc = null;
                ReferenceManager.KeyContext = null;
                ReferenceManager.KeyContextArray = null;
            }

            if (InventorPersistenceManager.ActivePartDoc != null)
            {
                InventorPersistenceManager.ActivePartDoc = null;
                ReferenceManager.KeyContext = null;
                ReferenceManager.KeyContextArray = null;
            }
        }

        void appEvents_OnActivateDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventNotHandled;
            try               
            {               
                if (userInterfaceManager.ActiveEnvironment.InternalName == "AMxAssemblyEnvironment")
                {
                    InventorPersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)DocumentObject;
                }

                else
                {
                    InventorPersistenceManager.ActiveAssemblyDoc = null;
                }
                
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public void Deactivate()
        {
            // TODO Dispose in InventorServices
            inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void UserInterfaceEvents_OnResetCommandBars(ObjectsEnumerator commandBars, NameValueMap context)
        {
            try
            {
                CommandBar commandBar;
                for (int commandBarCt = 1; commandBarCt <= commandBars.Count; commandBarCt++)
                {
                    commandBar = (Inventor.CommandBar)commandBars[commandBarCt];
                    if (commandBar.InternalName == commandBarInternalName)
                    {
                        commandBar.Controls.AddButton(dynamoAddinButton.ButtonDefinition, 0);
                        return;
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetEnvironments(ObjectsEnumerator environments, NameValueMap context)
        {
            try
            {
                Inventor.Environment environment;
                for (int i = 1; i <= environments.Count; i++)
                {
                    environment = (Inventor.Environment)environments[i];
                    if (environment.InternalName == "AMxAssemblyEnvironment")
                    {
                        environment.PanelBar.CommandBarList.Add(inventorApplication.UserInterfaceManager.CommandBars[commandBarInternalName]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetRibbonInterface(NameValueMap context)
        {
            try
            {
                //get the ribbon associated with part document
                Inventor.Ribbons ribbons = userInterfaceManager.Ribbons;
                Inventor.Ribbon assemblyRibbon = ribbons["Assembly"];

                //get the tabls associated with part ribbon
                RibbonTabs ribbonTabs = assemblyRibbon.RibbonTabs;
                RibbonTab assemblyRibbonTab = ribbonTabs["id_Assembly"];

                //create a new panel with the tab
                RibbonPanels ribbonPanels = assemblyRibbonTab.RibbonPanels;
                dynamoRibbonPanel = ribbonPanels.Add(ribbonPanelDisplayName, 
                                                     ribbonPanelInternalName,
                                                     "{DB59D9A7-EE4C-434A-BB5A-F93E8866E872}", 
                                                     "", 
                                                     false);

                CommandControls assemblyRibbonPanelCtrls = dynamoRibbonPanel.CommandControls;
                CommandControl copyUtilCmdBtnCmdCtrl = assemblyRibbonPanelCtrls.AddButton(dynamoAddinButton.ButtonDefinition, false, true, "", false);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// Automation is part of the ApplicationAddInServer implementation.
        /// We don't need this for now.
        /// </summary>
        public object Automation
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Automation is part of the ApplicationAddInServer implementation.
        /// </summary>
        public void ExecuteCommand(int CommandID)
        {
        }
        #endregion
    }
}
