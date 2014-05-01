using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using DSNodeServices;
using Dynamo.Models;
using Dynamo.Utilities;
using InventorLibrary.GeometryConversion;
using InventorServices.Persistence;
using InventorServices.Utilities;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace InventorLibrary.ModulePlacement
{
    [RegisterForTrace]
    public class Module
    {
        #region Private fields
        private List<WorkPointProxy> layoutWorkPointProxies = new List<WorkPointProxy>();
        private List<Inventor.WorkPoint> layoutWorkPoints = new List<Inventor.WorkPoint>();
        #endregion

        #region Private constructors
        private Module(List<Point> points)
        {
            InternalModulePoints = points;
        }
        #endregion

        #region Internal methods
        internal Module InternalPlaceModule()
        {
            //CreateInvLayout();
            return this;
        }

        internal void PlaceWorkGeometryForContsraints(PartComponentDefinition layoutComponentDefinition, ComponentOccurrence layoutOccurrence)
        {
            for (int i = 0; i < InternalModulePoints.Count; i++)
            {
                           
                WorkPoint workPoint = layoutComponentDefinition.WorkPoints.AddFixed(InternalModulePoints[i].ToPoint(), false);
                workPoint.Grounded = true;
                workPoint.Visible = false;
                //Inventor's API documentation is so bad!
                object workPointProxyObject;
                layoutOccurrence.CreateGeometryProxy(workPoint, out workPointProxyObject);
                LayoutWorkPointProxies.Add((WorkPointProxy)workPointProxyObject);
                LayoutWorkPoints.Add(workPoint);
            }

            LayoutWorkPlane = layoutComponentDefinition.WorkPlanes.AddByThreePoints(layoutWorkPoints[0], layoutWorkPoints[1], layoutWorkPoints[2]);
            LayoutWorkPlane.Grounded = true;
            LayoutWorkPlane.Visible = false;
            object wPlaneProxyObject;
            layoutOccurrence.CreateGeometryProxy(LayoutWorkPlane, out wPlaneProxyObject);
            ModuleWorkPlaneProxyAssembly = (WorkPlaneProxy)wPlaneProxyObject;
            
        }
        #endregion

        #region Internal properties
        internal List<Point> InternalModulePoints { get; set; }

        internal WorkPlane LayoutWorkPlane { get; set; }

        internal List<WorkPointProxy> LayoutWorkPointProxies
        {
            get { return layoutWorkPointProxies; }
            set { layoutWorkPointProxies = value; }
        }

        internal List<WorkPoint> LayoutWorkPoints
        {
            get { return layoutWorkPoints; }
            set { layoutWorkPoints = value; }
        }

        internal WorkPlaneProxy ModuleWorkPlaneProxyAssembly { get; set; }
        #endregion

        #region Public static constructors
        public static Module ByPoints(List<Point> points)
        {
            return new Module(points);
        }
        #endregion

        #region Public methods
        public Module PlaceModule()
        {
            return InternalPlaceModule();
        }
        #endregion
    }
}
