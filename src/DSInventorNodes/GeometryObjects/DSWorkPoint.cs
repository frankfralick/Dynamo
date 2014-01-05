using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Inventor;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using DSNodeServices;
using DynamoInventor;
using Dynamo.Models;
using Dynamo.Utilities;
using DSInventorNodes.GeometryConversion;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DSInventorNodes.GeometryObjects
{
    [RegisterForTrace]
    [ShortName("workPt")]
    class DSWorkPoint : AbstractGeometryObject
    {

        #region Internal properties
        internal WorkPoint InternalWorkPoint { get; private set; }
        #endregion

        #region Private constructors
        private DSWorkPoint(Inventor.WorkPoint workPt)
        {
            InternalWorkPoint = workPt;
        }

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


        public Point Point
        {
            get
            {
                return InternalWorkPoint.Point.ToPoint();
            }
        }

        #endregion

        #region Public static constructors

        public static DSWorkPoint ByCoordinates(double x, double y, double z)
        {
            return new DSWorkPoint(x, y, z);
        }

        public static DSWorkPoint ByPoint(Point pt)
        {
            return new DSWorkPoint(pt.X, pt.Y, pt.Z);
        }

        #endregion

        #region Internal static constructors
        internal static DSWorkPoint FromExisting(Inventor.WorkPoint pt)
        {
            return new DSWorkPoint(pt);
        }
        #endregion

        #region Tesselation

        #endregion


        internal static void MoveWorkPoint(double x, double y, double z, Inventor.WorkPoint wp)
        {
            Inventor.Point newLocation = InventorSettings.InventorApplication.TransientGeometry.CreatePoint(x, y, z);
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
            Inventor.Point point = InventorSettings.InventorApplication.TransientGeometry.CreatePoint(x, y, z);
            wp = compDef.WorkPoints.AddFixed(point, false);

            byte[] refKey = new byte[] { };
            wp.GetReferenceKey(ref refKey, (int)InventorSettings.KeyContext);

            //ComponentOccurrenceKeys.Add(refKey);
            return wp;
        }

        
    }
}
