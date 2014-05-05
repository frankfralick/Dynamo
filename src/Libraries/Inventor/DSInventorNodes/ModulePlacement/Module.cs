using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Runtime;
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
    [IsVisibleInDynamoLibrary(false)]
    public class Module
    {
        #region Private fields
        private bool reuseDuplicates = true;
        private List<WorkPointProxy> layoutWorkPointProxies = new List<WorkPointProxy>();
        private List<WorkPoint> layoutWorkPoints = new List<WorkPoint>();
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
            return this;
        }

        internal void PlaceWorkGeometryForContsraints(PartComponentDefinition layoutComponentDefinition, ComponentOccurrence layoutOccurrence, int moduleNumber)
        {
            PartDocument partDoc = (PartDocument)layoutComponentDefinition.Document;
            ReferenceKeyManager refKeyManager = partDoc.ReferenceKeyManager;

            for (int i = 0; i < InternalModulePoints.Count; i++)
            {
                WorkPoint workPoint;
                if (ReferenceKeyBinderModule.GetObjectFromTrace<Inventor.WorkPoint>(moduleNumber, i, refKeyManager, out workPoint))
                {
                    Inventor.Point newLocation = InventorPersistenceManager.InventorApplication.TransientGeometry.CreatePoint(InternalModulePoints[i].X,
                                                                                                                              InternalModulePoints[i].Y,
                                                                                                                              InternalModulePoints[i].Z);

                    workPoint.SetFixed(InternalModulePoints[i].ToPoint());
                }

                else
                {
                    workPoint = layoutComponentDefinition.WorkPoints.AddFixed(InternalModulePoints[i].ToPoint(), false);
                    ReferenceKeyBinderModule.SetObjectForTrace<WorkPoint>(moduleNumber, i, workPoint, ModuleUtilities.ReferenceKeysSorter);
                }

                //workPoint.Visible = false;

                object workPointProxyObject;
                layoutOccurrence.CreateGeometryProxy(workPoint, out workPointProxyObject);
                LayoutWorkPointProxies.Add((WorkPointProxy)workPointProxyObject);
                LayoutWorkPoints.Add(workPoint);
            }

            //If we will have more than 2 constraints, it will help assembly stability later
            //if we have a plane to constrain to first.
            if (InternalModulePoints.Count > 2)
            {
                
                WorkPlane workPlane;
                if (ReferenceKeyBinderModule.GetObjectFromTrace<Inventor.WorkPlane>(moduleNumber, InternalModulePoints.Count, refKeyManager, out workPlane))
                {
                    if (workPlane.DefinitionType == WorkPlaneDefinitionEnum.kThreePointsWorkPlane)
                    {
                        workPlane.SetByThreePoints(LayoutWorkPoints[0], LayoutWorkPoints[1], LayoutWorkPoints[2]);
                    }
                }
                else
                {
                    //If the first three points are colinear, adding a workplane will fail.  We will check the area of a triangle 
                    //described by the first three points. If the area is very close to 0, we can assume these points are colinear, and we should
                    //not attempt to construct a work plane from them.
                    Inventor.Point pt1 = LayoutWorkPoints[0].Point;
                    Inventor.Point pt2 = LayoutWorkPoints[1].Point;
                    Inventor.Point pt3 = LayoutWorkPoints[2].Point;
                    if (pt1.X * (pt2.Y - pt3.Y) + pt2.X * (pt3.Y - pt1.Y) + pt3.X * (pt1.Y - pt2.Y) > .0000001)
                    {
                        workPlane = layoutComponentDefinition.WorkPlanes.AddByThreePoints(LayoutWorkPoints[0], LayoutWorkPoints[1], LayoutWorkPoints[2], false);
                        ReferenceKeyBinderModule.SetObjectForTrace<WorkPlane>(moduleNumber, InternalModulePoints.Count, workPlane, ModuleUtilities.ReferenceKeysSorter);
                        workPlane.Grounded = true;
                        //workPlane.Visible = false;
                        LayoutWorkPlane = workPlane;
                        object wPlaneProxyObject;
                        layoutOccurrence.CreateGeometryProxy(workPlane, out wPlaneProxyObject);
                        ModuleWorkPlaneProxyAssembly = (WorkPlaneProxy)wPlaneProxyObject; 
                    }
                    
                }  
            }
        }
        #endregion

        #region Internal properties


        internal int GeometryMapIndex { get; set; }

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

        internal bool ReuseDuplicates
        {
            get { return reuseDuplicates; }
            set { reuseDuplicates = value; }
        }
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
