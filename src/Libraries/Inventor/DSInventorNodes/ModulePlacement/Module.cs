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
using DSInventorNodes.GeometryConversion;
using InventorServices.Persistence;
using InventorServices.Utilities;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace DSInventorNodes.ModulePlacement
{
    [RegisterForTrace]
    public class Module
    {
        #region Private constructors
        private Module(List<Point> points)
        {
            InternalModulePoints = points;
        }
        #endregion

        #region Private methods
        private Module InternalPlaceModule()
        {
            CreateInvLayout();
            return this;
        }

        private void CreateInvLayout()
        {
            CreateLayoutPartFile();
        }

        //TODO: Need to create overloads with part template file as an argument in case someone wants to specify a template.
        private void CreateLayoutPartFile()
        {
            //temp for testing
            string partTemplateFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2014\Templates\Standard.ipt";
            string LayoutPartPath = "C:\\Users\\frankfralick\\Documents\\Inventor\\Dynamo 2014\\Layout.ipt";
            //TODO This is just for early testing of everything.  This will get set and managed elsewhere I think.
            if (!System.IO.File.Exists(LayoutPartPath))
            {
                PartDocument layoutPartDoc = (PartDocument)InventorPersistenceManager.InventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, partTemplateFile, true);
                layoutPartDoc.SaveAs(LayoutPartPath, false);
                layoutPartDoc.Close();
            }

        }
        #endregion

        #region Internal properties
        List<Point> InternalModulePoints { get; set; }
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
