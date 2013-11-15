using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

				}
				else
				{
					//Not actively in an assembly, shouldn't be possible based on Environments set up in StandardAddInServer.
					MessageBox.Show("Something terrible happened.");
				}		
			}

			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}
}
