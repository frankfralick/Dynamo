using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dynamo;
using Dynamo.Nodes;
using Dynamo.ViewModels;

namespace DynamoInventor
{
    class DynamoInventorViewModel : DynamoViewModel    
    {
        public DynamoInventorViewModel(DynamoController controller, string commandFilePath) : base(controller, commandFilePath) { }

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
