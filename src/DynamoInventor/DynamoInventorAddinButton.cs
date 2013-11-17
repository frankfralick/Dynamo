﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Inventor;

using Dynamo;
using Dynamo.Controls;
using Dynamo.FSchemeInterop;
using Dynamo.Utilities;

namespace DynamoInventor
{
    internal class DynamoInventorAddinButton : Button
    {

        //Dynamo
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
                //For proof of concept's sake we will just worry with the assembly environment for now.
				//Check to make sure an assembly file is active.
				if (InventorApplication.ActiveEditObject is AssemblyDocument)
				{
					//Start Dynamo!  
                    //get window handle
                    IntPtr mwHandle = Process.GetCurrentProcess().MainWindowHandle;

                    string versionYear = InventorApplication.SoftwareVersion.DisplayVersion;

                    env = new ExecutionEnvironment();

                    dynamoView = new DynamoView() { DataContext = "None" };

                    //set window handle and show dynamo
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
                    //dynamoView.Closed += dynamoView_Closed;

				}
				else
				{
					//Not actively in an assembly, shouldn't be possible based on Environments set up in StandardAddInServer.
					System.Windows.Forms.MessageBox.Show("Something terrible happened.");
				}		
			}

			catch(Exception e)
			{
                System.Windows.Forms.MessageBox.Show(e.ToString());
			}
		}
	}
}
