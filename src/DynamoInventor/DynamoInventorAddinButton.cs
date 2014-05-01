﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Inventor;

using Dynamo;
using Dynamo.Controls;
using Dynamo.FSchemeInterop;
using Dynamo.UpdateManager;
using Dynamo.Utilities;
using InventorServices.Persistence;

namespace DynamoInventor
{
    internal class DynamoInventorAddinButton : Button
    {
        public static ExecutionEnvironment env;
        private static DynamoView dynamoView;
        private DynamoController dynamoController;
        private static bool isRunning = false;
        public static double? dynamoViewX = null;
        public static double? dynamoViewY = null;
        public static double? dynamoViewWidth = null;
        public static double? dynamoViewHeight = null;
        private bool handledCrash = false;

		public DynamoInventorAddinButton(string displayName, 
                                         string internalName, 
                                         CommandTypesEnum commandType, 
                                         string clientId, 
                                         string description, 
                                         string tooltip, 
                                         Icon standardIcon, 
                                         Icon largeIcon, 
                                         ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{		
		}

		public DynamoInventorAddinButton(string displayName, 
                                         string internalName, 
                                         CommandTypesEnum commandType, 
                                         string clientId, 
                                         string description, 
                                         string tooltip, 
                                         ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{		
		}

		override protected void ButtonDefinition_OnExecute(NameValueMap context)
		{
			try
			{
                //TODO Refactor Dynamo initialization steps out. 

                if (isRunning == false)
                {
                    //For right now we are just worried about Dynamo in the Assembly environment.
                    if (InventorPersistenceManager.InventorApplication.ActiveDocument is AssemblyDocument)
                    {
                        //Start Dynamo!  
                        IntPtr mwHandle = Process.GetCurrentProcess().MainWindowHandle;

                        string inventorContext = "Inventor " + InventorPersistenceManager.InventorApplication.SoftwareVersion.DisplayVersion;

                        //BaseUnit.HostApplicationInternalAreaUnit = DynamoAreaUnit.SquareFoot;
                        //BaseUnit.HostApplicationInternalLengthUnit = DynamoLengthUnit.DecimalFoot;
                        //BaseUnit.HostApplicationInternalVolumeUnit = DynamoVolumeUnit.CubicFoot;

                        //var logger = new DynamoLogger();
                        //var updateManager = new UpdateManager.UpdateManager(logger);
                        //dynamoController = new DynamoController_Revit(Updater, context, updateManager, logger);

                        //// Generate a view model to be the data context for the view
                        //dynamoController.DynamoViewModel = new DynamoRevitViewModel(dynamoController, null);
                        //dynamoController.DynamoViewModel.RequestAuthentication += ((DynamoController_Revit)dynamoController).RegisterSingleSignOn;
                        //dynamoController.DynamoViewModel.CurrentSpaceViewModel.CanFindNodesFromElements = true;
                        //dynamoController.DynamoViewModel.CurrentSpaceViewModel.FindNodesFromElements = ((DynamoController_Revit)dynamoController).FindNodesFromSelection;

                        //// Register the view model to handle sign-on requests
                        //dynSettings.Controller.DynamoViewModel.RequestAuthentication += ((DynamoController_Revit)dynamoController).RegisterSingleSignOn;

                        //dynamoController.VisualizationManager = new VisualizationManagerRevit();

                        //dynamoView = new DynamoView { DataContext = dynamoController.DynamoViewModel };
                        //dynamoController.UIDispatcher = dynamoView.Dispatcher;

                        DynamoLogger logger = new DynamoLogger();
                        var updateManager = new UpdateManager(logger);

                        dynamoController = new DynamoController_Inventor(inventorContext, updateManager, logger);

                        dynamoController.DynamoViewModel = new DynamoInventorViewModel(dynamoController, null);
                        //dynamoController.DynamoViewModel.RequestAuthentication += ((DynamoController_Inventor)dynamoController).RegisterSingleSignOn;
                        //dynamoController.DynamoViewModel.CurrentSpaceViewModel.CanFindNodesFromElements = true;
                        //dynamoController.DynamoViewModel.CurrentSpaceViewModel.FindNodesFromElements = ((DynamoController_Revit)dynamoController).FindNodesFromSelection;


                        //dynamoController.VisualizationManager = new VisualizationManagerRevit();
                        dynamoView = new DynamoView() { DataContext = dynamoController.DynamoViewModel };
                        dynamoController.UIDispatcher = dynamoView.Dispatcher;

                        new WindowInteropHelper(dynamoView).Owner = mwHandle;

                        handledCrash = false;

                        dynamoView.WindowStartupLocation = WindowStartupLocation.Manual;

                        Rectangle bounds = Screen.PrimaryScreen.Bounds;
                        dynamoView.Left = dynamoViewX ?? bounds.X;
                        dynamoView.Top = dynamoViewY ?? bounds.Y;
                        dynamoView.Width = dynamoViewWidth ?? 1000.0;
                        dynamoView.Height = dynamoViewHeight ?? 800.0;

                        dynamoView.Show();

                        //dynamoView.Dispatcher.UnhandledException -= DispatcherOnUnhandledException;
                        //dynamoView.Dispatcher.UnhandledException += DispatcherOnUnhandledException;
                        //dynamoView.Closing += dynamoView_Closing;
                        dynamoView.Closed += dynamoView_Closed;
                        isRunning = true;
                    }
                }

                else if (isRunning == true)
                {
                    System.Windows.Forms.MessageBox.Show("Dynamo is already running.");
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("Something terrible happened.");
                }		
			}

			catch(Exception e)
			{
                System.Windows.Forms.MessageBox.Show(e.ToString());
			}
		}

        /// <summary>
        /// Executes after Dynamo closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dynamoView_Closed(object sender, EventArgs e)
        {
            dynamoView = null;
            isRunning = false;
        }
	}
}
