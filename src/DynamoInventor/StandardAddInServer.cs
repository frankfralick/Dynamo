using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Inventor;

namespace DynamoInventor
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("476f38a1-75f3-450b-a75a-6f030bf012a8")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        #region Data Members
        // Inventor application object.
        private Inventor.Application invApp;
        private DynamoAddinButton dynamoAddinButton;
        private UserInterfaceEvents m_userInterfaceEvents;
        RibbonPanel dynamoRibbonPanel;

        private string commandBarInternalName = "Dynamo:InventorDynamo:DynamoCommandBar";
        private string ribbonPanelInternalName = "Dynamo:InventorDynamo:DynamoRibbonPanel";
        private string ribbonPanelDisplayName = "Dynamo";


        private Inventor.UserInterfaceEventsSink_OnResetCommandBarsEventHandler UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetEnvironmentsEventHandler UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

        #endregion

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            invApp = addInSiteObject.Application;

            Button.InventorApplication = invApp;

            //initialize event delegates
            m_userInterfaceEvents = invApp.UserInterfaceManager.UserInterfaceEvents;

            UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
            m_userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

            UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
            m_userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

            UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
            m_userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;
            
            
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
                    //if (commandBar.InternalName == "Beck:InventorCopy:AssUtilToolbar")
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
