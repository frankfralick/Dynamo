using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Inventor;

namespace DynamoInventor
{
    internal class DynamoAddinButton : Button
    {
		public DynamoAddinButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{		
		}

		public DynamoAddinButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
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
