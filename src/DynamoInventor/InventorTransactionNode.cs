using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Dynamo.Models;

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
    class InventorTransactionNode : NodeModel
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

        // This list contains the elements of the current recurvise execution
        public List<byte[]> ComponentOccurrenceKeys
        {
            get
            {
                while (elements.Count <= _runCount)
                    elements.Add(new List<byte[]>());
                return elements[_runCount];
            }
        }
            
    }
}
