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
using InventorLibrary.API;
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
        private List<InvWorkPoint> layoutWorkPoints = new List<InvWorkPoint>();
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

        internal void PlaceWorkGeometryForContsraints(InvPartComponentDefinition layoutComponentDefinition, ComponentOccurrence layoutOccurrence)
        {
            for (int i = 0; i < InternalModulePoints.Count; i++)
            {
                           
                //WorkPoint workPoint = layoutComponentDefinition.WorkPoints.AddFixed(InternalModulePoints[i].ToPoint(), false);
                InvWorkPoint workPoint = layoutComponentDefinition.WorkPoints.AddFixed(InternalModulePoints[i], false);
                workPoint.Grounded = true;
                workPoint.Visible = false;
                //Inventor's API documentation is so bad!
                object workPointProxyObject;
                layoutOccurrence.CreateGeometryProxy(workPoint.WorkPointInstance, out workPointProxyObject);
                LayoutWorkPointProxies.Add((WorkPointProxy)workPointProxyObject);
                LayoutWorkPoints.Add(workPoint);
            }
            if (InternalModulePoints.Count > 2)
            {
                Point point1 = LayoutWorkPoints[0].Point.PointInstance.ToPoint();
                Point point2 = LayoutWorkPoints[1].Point.PointInstance.ToPoint();
                Point point3 = LayoutWorkPoints[2].Point.PointInstance.ToPoint();
                LayoutWorkPlane = layoutComponentDefinition.WorkPlanes.AddByThreePoints(LayoutWorkPoints[0], LayoutWorkPoints[1], LayoutWorkPoints[2], false);
                LayoutWorkPlane.Grounded = true;
                LayoutWorkPlane.Visible = false;
                object wPlaneProxyObject;
                layoutOccurrence.CreateGeometryProxy(LayoutWorkPlane.WorkPlaneInstance, out wPlaneProxyObject);
                ModuleWorkPlaneProxyAssembly = (WorkPlaneProxy)wPlaneProxyObject; 
            }
            
            
        }
        #endregion

        #region Internal properties
        internal List<Point> InternalModulePoints { get; set; }

        internal InvWorkPlane LayoutWorkPlane { get; set; }

        internal List<WorkPointProxy> LayoutWorkPointProxies
        {
            get { return layoutWorkPointProxies; }
            set { layoutWorkPointProxies = value; }
        }

        internal List<InvWorkPoint> LayoutWorkPoints
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
