using System;
using Inventor;

using Dynamo.Models;
using Dynamo.Utilities;
using Microsoft.FSharp.Collections;
using Value = Dynamo.FScheme.Value;
using Dynamo.FSchemeInterop;
using DynamoInventor;


namespace Dynamo.Nodes
{
    [NodeName("WorkPoint")]
    [NodeCategory(BuiltinNodeCategories_Inventor.INVENTOR_WORKFEATURES)]
    [NodeDescription("Place a work point given a coordinate.")]
    public class WorkPoint : InventorTransactionNodeWithOneOutput
    {
        public WorkPoint()
        {
            InPortData.Add(new PortData("x", "X coordinate", typeof(Value.Number)));
            InPortData.Add(new PortData("y", "Y coordinate", typeof(Value.Number)));
            InPortData.Add(new PortData("z", "Z coordinate", typeof(Value.Number)));
            OutPortData.Add(new PortData("wp", "The resulting work point.", typeof(Value.Container)));

            RegisterAllPorts();
        }

        public override Value Evaluate(FSharpList<Value> args)
        {
            double x = ((Value.Number)args[0]).Item;
            double y = ((Value.Number)args[1]).Item;
            double z = ((Value.Number)args[2]).Item;

            Inventor.Application invApp = DynamoInventor.DynamoInventorAddinButton.InventorApplication;
            Inventor.WorkPoint wp;
          
            //If this node has been run already and there is something in ComponentOccurrenceKeys,
            //then modify the object based on the inputs.
            //Could input values be stored so that re-evaluation could be skipped?
            if (ComponentOccurrenceKeys.Count != 0)
            {
                if (true) //If we find the byte[], and can bind to the object, modify it.
                {
                    
                }
                else //Otherwise, create a new object.
                {

                }
            }

            //Otherwise we need to create the thing this node is trying to make, and assign its
            //ReferenceKey byte[] to ComponentOccurrenceKeys[0].
            else
            {
                AssemblyDocument assDoc = (AssemblyDocument)invApp.ActiveDocument;
                AssemblyComponentDefinition compDef = (AssemblyComponentDefinition)assDoc.ComponentDefinition;
                Point point = invApp.TransientGeometry.CreatePoint(x, y, z);
                wp = compDef.WorkPoints.AddFixed(point, false);
                
            }
            

            return Value.NewContainer(wp);
        }
    }
}
