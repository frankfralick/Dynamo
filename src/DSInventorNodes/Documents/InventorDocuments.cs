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

namespace DSInventorNodes.Documents
{
    [RegisterForTrace]
    public class InventorDocuments
    {

        #region Internal properties

        #endregion

        #region Private constructors
        private InventorDocuments()
        {

        }
      
        #endregion

        #region Private mutators
        private InventorAssemblyDocument InternalAddAssemblyDocument()
        {
            string assemblyTemplateFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2013\Templates\Standard.iam";
            Inventor.Application invApp = (Inventor.Application)InventorServices.Persistence.InventorPersistenceManager.InventorApplication;
            Inventor.AssemblyDocument assemblyDocument = (Inventor.AssemblyDocument)invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, assemblyTemplateFile, true);          
            return InventorAssemblyDocument.ByAssemblyDocument(assemblyDocument);        
        }
        #endregion

        #region Public properties

        #endregion

        #region Public static constructors
        public static InventorDocuments GetDocumentsInterface()
        {
            return new InventorDocuments();
        }
        #endregion

        #region Public methods

        public InventorAssemblyDocument AddAssemblyDefaultTemplate()
        {
            return InternalAddAssemblyDocument();
        }
        #endregion

        #region Internal static constructors

        #endregion

    }
}
