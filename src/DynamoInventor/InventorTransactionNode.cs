using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FSharp.Collections;
using Inventor;

using Dynamo.Models;
using Value = Dynamo.FScheme.Value;

namespace DynamoInventor
{
    /// <summary>
    /// This class will be the parent class of nodes that create or modify objects
    /// within Inventor.  One of the main functions of this class is to keep track
    /// of whether or not a node is being run for the first time, how many runs
    /// have occured etc.  Nodes that inherit from this that create objects will
    /// have object creation logic the first time, but on subsequent runs, the object
    /// needs to be modified rather than created.
    /// </summary>
    public abstract class InventorTransactionNode : NodeModel
    {
        private int _runCount;

        protected Inventor.AssemblyDocument AssemblyDocument
        {
            get { return InventorSettings.ActiveAssemblyDoc; }
        }

        private List<List<byte[]>> elements
        {
            get
            {
                return InventorSettings.ComponentOccurrencesContainers.Peek()[GUID];
            }
        }

        //// This list contains the elements of the current recurvise execution
        //public List<byte[]> ComponentOccurrenceKeys
        //{
        //    get
        //    {
        //        while (elements.Count <= _runCount)
        //            elements.Add(new List<byte[]>());
        //        return elements[_runCount];
        //    }
        //}

        // This works for updating objects, but something is wrong 
        //with this.
        public List<byte[]> compOccKeys = new List<byte[]>();
        public List<byte[]> ComponentOccurrenceKeys
        {
            get
            { 
                return compOccKeys;
            }
            set { value = compOccKeys; }
        }



        public IEnumerable<byte[]> AllComponentOccurrenceKeys
        {
            get
            {
                return elements.SelectMany(x => x);
            }
        }

        protected InventorTransactionNode()
        {
            ArgumentLacing = LacingStrategy.Longest;

            //In DynamoRevit there is 'RegisterAllElementsDeleteHook' and some event stuff here
            //Don't understand the overlap between ElementsContainer methods and those in 
            //RevitTransactionNode.
        }

        protected override void OnEvaluate()
        {
            base.OnEvaluate();

            _runCount++;
        }
    }

    public abstract class InventorTransactionNodeWithOneOutput : InventorTransactionNode
    {
        public override void Evaluate(FSharpList<Value> args, Dictionary<PortData, Value> outPuts)
        {
            outPuts[OutPortData[0]] = Evaluate(args);
        }

        public abstract Value Evaluate(FSharpList<Value> args);
    }
}
