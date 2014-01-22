using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Inventor;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using DSNodeServices;
using Dynamo.Models;
using Dynamo.Utilities;
using DSInventorNodes.GeometryConversion;
using InventorServices.Persistence;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DSInventorNodes
{
    [RegisterForTrace]
    public class InvPoint
    {
        #region Internal properties
        internal Inventor.Point InternalPoint { get; set; }

        internal double InternalX { get; set; }

        internal double InternalY { get; set; }

        internal double InternalZ { get; set; }
        #endregion

        #region Private constructors
        private InvPoint(InvPoint invPoint)
        {
            InternalPoint = invPoint.InternalPoint;
        }

        private InvPoint(Inventor.Point invPoint)
        {
            InternalPoint = invPoint;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.Point PointInstance
        {
            get { return InternalPoint; }
            set { InternalPoint = value; }
        }

        public double X
        {
            get { return InternalX; }
            set { InternalX = value; }
        }

        public double Y
        {
            get { return InternalY; }
            set { InternalY = value; }
        }

        public double Z
        {
            get { return InternalZ; }
            set { InternalZ = value; }
        }

        #endregion

        #region Public static constructors
        public static InvPoint ByInvPoint(InvPoint invPoint)
        {
            return new InvPoint(invPoint);
        }
        public static InvPoint ByInvPoint(Inventor.Point invPoint)
        {
            return new InvPoint(invPoint);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
