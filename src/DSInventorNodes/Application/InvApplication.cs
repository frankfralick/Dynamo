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
using Application = Autodesk.DesignScript.Geometry.Application;

namespace DSInventorNodes.Application
{
    [RegisterForTrace]
    public class InvApplication
    {

        #region Internal properties
        internal Inventor.Application InternalApplication { get; set; }
        #endregion

        #region Private constructors
        private InvApplication()
        {
            InternalSetApplication();
        }
        #endregion

        #region Private mutators
        private void InternalSetApplication()
        {
            InternalApplication = InventorServices.Persistence.InventorPersistenceManager.InventorApplication;
        }
        #endregion

        #region Public properties
        public Inventor.Application ApplicationInterface
        {
            get { return (Inventor.Application)InternalApplication; }
        }

        public string TestString
        {
            get { return "Test property value"; }
        }
        #endregion

        #region Public static constructors
        public static InvApplication GetInventorApplication()
        {
            return new InvApplication();
        }
        #endregion

        #region Public methods
        
        #endregion

        #region Internal static constructors
        #endregion

        #region Tesselation
        #endregion

    }
}
