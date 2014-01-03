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

namespace DSInventorNodes.GeometryObjects
{
    [RegisterForTrace]
    class DSWorkPoint : AbstractGeometryObject
    {

        #region Internal properties
        internal WorkPoint InternalWorkPoint { get; private set; }
        #endregion

        #region Private constructors
        public DSWorkPoint(double x, double y, double z)
        {
            //Inventor.WorkPoint wp;
            InternalWorkPoint = CreateNewWorkPoint(x, y, z);
        }
        #endregion

        #region Private mutators
        #endregion

        #region Public properties
        public double X
        {
            get { return InternalWorkPoint.Point.X; }
            set { MoveWorkPoint(value, Y, Z, InternalWorkPoint); }
        }
        public double Y
        {
            get { return InternalWorkPoint.Point.Y; }
            set { MoveWorkPoint(X, value, Z, InternalWorkPoint); }
        }
        public double Z
        {
            get { return InternalWorkPoint.Point.Z; }
            set { MoveWorkPoint(X, Y, value, InternalWorkPoint); }
        }
        #endregion

        #region Public static constructors

        public static DSWorkPoint ByCoordinates(double x, double y, double z)
        {
            return new DSWorkPoint(x, y, z);
        }

        #endregion

        #region Internal static constructors
        #endregion

        #region Tesselation

        #endregion


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
