using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Inventor;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Runtime;
using DSNodeServices;
using Dynamo.Models;
using Dynamo.Utilities;
using InventorLibrary.GeometryConversion;
using InventorServices.Persistence;
using Point = Autodesk.DesignScript.Geometry.Point;
using SimpleInjector;

namespace InventorLibrary.WorkGeometry
{
    [IsVisibleInDynamoLibrary(true)]
    public class WorkPoint
    {
        private Point point;
        private IObjectBinder binder;

        public WorkPoint(Point point, IObjectBinder binder)
        {
            this.point = point;
            this.binder = binder;
            Inventor.WorkPoint wp;
            if (this.binder.GetObjectFromTrace<Inventor.WorkPoint>(out wp))
            {
                InternalWorkPoint = wp;
                AssemblyWorkPointDef wpDef = (AssemblyWorkPointDef)wp.Definition;
                wpDef.Point = point.ToPoint();
            }

            else
            {
                wp = PersistenceManager.ActiveAssemblyDoc.ComponentDefinition.WorkPoints.AddFixed(point.ToPoint(), false);
                InternalWorkPoint = wp;
                this.binder.SetObjectForTrace<WorkPoint>(this.InternalWorkPoint);
            }
        }
        #region Private fields
        #endregion

        #region Internal properties   
        internal Inventor.WorkPoint InternalWorkPoint { get; set; }
        #endregion

        #region Private constructors
        #endregion

        #region Private methods    
        #endregion

        #region Public static constructors
        public static WorkPoint ByPoint(Point point)
        {
            var binder = PersistenceManager.IoC.GetInstance<IObjectBinder>();
            return new WorkPoint(point, binder);
        }
        #endregion

        #region Public methods
        #endregion

        
    }
}
