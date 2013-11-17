using System;

using Dynamo.Models;
using Dynamo.Utilities;
using Microsoft.FSharp.Collections;
using Value = Dynamo.FScheme.Value;
using Dynamo.FSchemeInterop;
using Inventor;

namespace Dynamo.Nodes
{
    [NodeName("WorkPoint")]
    [NodeCategory(BuiltinNodeCategories_Inventor.INVENTOR_WORKFEATURES)]
    [NodeDescription("Place a work point given a coordinate.")]
    public class WorkPoint : NodeWithOneOutput
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
            //Need to add standard way to verify active environment type; assuming assembly for now.
            //Check what they are doing when nodes fail.
            AssemblyDocument assDoc = (AssemblyDocument)invApp.ActiveDocument;
            AssemblyComponentDefinition compDef = (AssemblyComponentDefinition)assDoc.ComponentDefinition;
            Point point = invApp.TransientGeometry.CreatePoint(x, y, z);
            Inventor.WorkPoint wp = compDef.WorkPoints.AddFixed(point, false);

            return Value.NewContainer(wp);
        }
    }
}
