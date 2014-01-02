using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;

using DynamoInventor.Properties;


namespace DynamoInventor
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("476f38a1-75f3-450b-a75a-6f030bf012a8")]
    public class DynamoInventor : Inventor.ApplicationAddInServer
    {
        #region Data Members

        private Inventor.Application invApp;
        private DynamoInventorAddinButton dynamoAddinButton;
        private UserInterfaceEvents m_userInterfaceEvents;
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

        public DynamoInventor()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // The FirstTime flag indicates if the addin is loaded for the first time.
            try
            {
                // Initialize AddIn members.
                invApp = addInSiteObject.Application;

                //TODO Move all references to Button.InventorApplication to InventorSettings.
                Button.InventorApplication = invApp;
                InventorSettings.InventorApplication = invApp;

                //TODO Fix this, this is never going to work, a document isn't active when this is called.
                if (InventorSettings.InventorApplication.ActiveDocument is AssemblyDocument)
                {
                    InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
                    InventorSettings.KeyManager = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager;
                    InventorSettings.KeyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                }

                //initialize event delegates
                m_userInterfaceEvents = invApp.UserInterfaceManager.UserInterfaceEvents;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
                m_userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
                m_userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
                m_userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

                appEvents = invApp.ApplicationEvents;
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

                CommandCategory assemblyUtilitiesCategory = invApp.CommandManager.CommandCategories.Add(commandCategoryDisplayName, commandCategoryInternalName, addInCLSID);
                assemblyUtilitiesCategory.Add(dynamoAddinButton.ButtonDefinition);

                if (firstTime == true)
                {
                    //access user interface manager
                    UserInterfaceManager userInterfaceManager;
                    userInterfaceManager = invApp.UserInterfaceManager;

                    InterfaceStyleEnum interfaceStyle;
                    interfaceStyle = userInterfaceManager.InterfaceStyle;

                    //Is classic interface style a thing still?
                    if (interfaceStyle == InterfaceStyleEnum.kClassicInterface)
                    {
                        CommandBar assemblyUtilityCommandBar;
                        assemblyUtilityCommandBar = userInterfaceManager.CommandBars.Add(commandBarDisplayName,
                                                                                         commandBarInternalName,
                                                                                         CommandBarTypeEnum.kRegularCommandBar,
                                                                                         addInCLSID);
                    }

                    //Otherwise we have kRibbonInterface
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
            if (InventorSettings.ActiveAssemblyDoc != null)
            {
                //The user has changed documents, clear all this out.
                InventorSettings.ActiveAssemblyDoc = null;
                InventorSettings.KeyContext = null;
                InventorSettings.KeyContextArray = null;
            }
        }

        void appEvents_OnActivateDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventNotHandled;
            try
            {
                InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)DocumentObject;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            invApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
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
                for (int environmentCt = 1; environmentCt <= environments.Count; environmentCt++)
                {
                    environment = (Inventor.Environment)environments[environmentCt];
                    if (environment.InternalName == "AMxAssemblyEnvironment")
                    {
                        //make this command bar accessible in the panel menu for the assembly environment.
                        environment.PanelBar.CommandBarList.Add(invApp.UserInterfaceManager.CommandBars[commandBarInternalName]);
                        return;
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
                UserInterfaceManager userInterfaceManager;
                userInterfaceManager = invApp.UserInterfaceManager;

                //get the ribbon associated with part document
                Inventor.Ribbons ribbons;
                ribbons = userInterfaceManager.Ribbons;

                Inventor.Ribbon assemblyRibbon;
                assemblyRibbon = ribbons["Assembly"];

                //get the tabls associated with part ribbon
                RibbonTabs ribbonTabs;
                ribbonTabs = assemblyRibbon.RibbonTabs;

                RibbonTab assemblyRibbonTab;
                assemblyRibbonTab = ribbonTabs["id_Assembly"];

                //create a new panel with the tab
                RibbonPanels ribbonPanels;
                ribbonPanels = assemblyRibbonTab.RibbonPanels;

                dynamoRibbonPanel = ribbonPanels.Add(ribbonPanelDisplayName, ribbonPanelInternalName,
                                                             "{DB59D9A7-EE4C-434A-BB5A-F93E8866E872}", "", false);

                CommandControls assemblyRibbonPanelCtrls;
                assemblyRibbonPanelCtrls = dynamoRibbonPanel.CommandControls;

                CommandControl copyUtilCmdBtnCmdCtrl;
                copyUtilCmdBtnCmdCtrl = assemblyRibbonPanelCtrls.AddButton(dynamoAddinButton.ButtonDefinition, false, true, "", false);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion

    }
}
