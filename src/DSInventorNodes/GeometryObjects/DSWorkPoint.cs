using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Inventor;
using Autodesk.DesignScript.Interfaces;

using DSNodeServices;
using DynamoInventor;
using Dynamo.Models;
using Dynamo.Utilities;
using ProtoCore.BuildData;
using ArrayNode = ProtoCore.AST.AssociativeAST.ArrayNode;
using Node = ProtoCore.AST.Node;
using Operator = ProtoCore.DSASM.Operator;

namespace DSInventorNodes.GeometryObjects
{
    [RegisterForTrace]
    [IsDesignScriptCompatible]
    class DSWorkPoint
    {
        #region Private constructors
        public DSWorkPoint(double x, double y, double z)
        {
            Inventor.WorkPoint wp;
            wp = CreateNewWorkPoint(x, y, z);
        }
        #endregion

        #region Public static constructors
        //public static DSWorkPoint ByCoordinates(double x, double y, double z)
        //{
        //    return new DSWorkPoint(x, y, z);
        //}
        #endregion

        //public DSWorkPoint(double x, double y, double z)
        //{
        //    ArgumentLacing = LacingStrategy.Disabled;
        //}

        //public DSWorkPoint(string userCode, Guid guid, WorkspaceModel workSpace, double XPos, double YPos)
        //{
        //    ArgumentLacing = LacingStrategy.Disabled;
        //    this.X = XPos;
        //    this.Y = YPos;
        //    this.GUID = guid;
        //    this.WorkSpace = workSpace;
        //}


        internal static void MoveWorkPoint(double x, double y, double z, Inventor.WorkPoint wp)
        {
            Point newLocation = InventorSettings.InventorApplication.TransientGeometry.CreatePoint(x, y, z);
            AssemblyWorkPointDef wpDef = (AssemblyWorkPointDef)wp.Definition;
            wpDef.Point = newLocation;
        }

        internal Inventor.WorkPoint CreateNewWorkPoint(double x, double y, double z)
        {
            //this.VerifyContextSettings();
            Inventor.WorkPoint wp;
            AssemblyDocument assDoc = InventorSettings.ActiveAssemblyDoc;
            //AssemblyDocument assDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
            AssemblyComponentDefinition compDef = (AssemblyComponentDefinition)assDoc.ComponentDefinition;
            Point point = InventorSettings.InventorApplication.TransientGeometry.CreatePoint(x, y, z);
            wp = compDef.WorkPoints.AddFixed(point, false);

            byte[] refKey = new byte[] { };
            wp.GetReferenceKey(ref refKey, (int)InventorSettings.KeyContext);

            //ComponentOccurrenceKeys.Add(refKey);
            return wp;
        }

        
    }
}
