﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Dynamo;
using Dynamo.Nodes;
using Dynamo.ViewModels;

using System.Windows.Forms;

namespace DynamoInventor
{
    class DynamoInventorViewModel : DynamoViewModel    
    {
        public DynamoInventorViewModel(DynamoController controller, string commandFilePath) : base(controller, commandFilePath) 
        {
            WorkspaceViewModel currentWorkspace = this.Workspaces.FirstOrDefault(p => p.IsCurrentSpace == true);
            currentWorkspace.Model.WorkspaceSaved += Model_WorkspaceSaved;    
        }

        void Model_WorkspaceSaved(Dynamo.Models.WorkspaceModel model)
        {
            //If the saved model has nodes that can bind to live objects, 
            //we want to save the KeyContext and the object reference keys
            //by node.  Then we can subscribe to the model opening event,
            //look up the binding info in AppData if it exists, and attempt to
            //bind back our Dynamo model to the Inventor model like a boss.

            //Get the nodes collection.
            List<Dynamo.Models.NodeModel> inventorNodes = this.Model.Nodes;
            
            //Setup the data to store.
            string testDummyData = "The quick brown fox jumped over the lazy dog.";
            AssemblyDocument assDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
            if (InventorUtilities.CreatePrivateStorageAndStream((Document)assDoc, InventorSettings.DynamoStorageName, "Test", testDummyData))
            {
                System.Windows.Forms.MessageBox.Show("Wrote to stream successfully.");
            }

            string testReadResult;
            if (InventorUtilities.ReadPrivateStorageAndStream((Document)assDoc, InventorSettings.DynamoStorageName, "Test", out testReadResult))
            {
                System.Windows.Forms.MessageBox.Show(testReadResult);
            }
        }

        public override bool CanRunDynamically
        {
            get
            {
                //we don't want to be able to run
                //dynamically if we're in debug mode
                bool manTranRequired = false; //For now in Inventor, nothing requires a "manual transaction".
                return !manTranRequired && !debug;
            }
            set
            {
                canRunDynamically = value;
                RaisePropertyChanged("CanRunDynamically");
            }
        }

        //public override bool DynamicRunEnabled
        //{
        //    get
        //    {
        //        return dynamicRun;
        //    }
        //    set
        //    {
        //        dynamicRun = value;
        //        RaisePropertyChanged("DynamicRunEnabled");
        //    }
        //}

        public override bool DynamicRunEnabled
        {
            get
            {
                return dynamicRun;
            }
            set
            {
                dynamicRun = value;
                RaisePropertyChanged("DynamicRunEnabled");
            }
        }

        public override bool RunInDebug
        {
            get { return debug; }
            set
            {
                debug = value;

                //toggle off dynamic run
                CanRunDynamically = !debug;

                if (debug)
                    DynamicRunEnabled = false;

                RaisePropertyChanged("RunInDebug");
            }
        }

        //public override Function CreateFunction(
        //    IEnumerable<string> inputs,
        //    IEnumerable<string> outputs,
        //    FunctionDefinition functionDefinition)
        //{
        //    if (functionDefinition.WorkspaceModel.Nodes.Any(x => x is RevitTransactionNode)
        //        || functionDefinition.Dependencies.Any(d => d.WorkspaceModel.Nodes.Any(x => x is RevitTransactionNode)))
        //    {
        //        //return new FunctionWithRevit(inputs, outputs, functionDefinition);
        //    }
        //    return base.CreateFunction(inputs, outputs, functionDefinition);
        //}

    }
}
