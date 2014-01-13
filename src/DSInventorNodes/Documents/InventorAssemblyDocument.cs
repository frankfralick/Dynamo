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
    public class InventorAssemblyDocument
    {

        #region Internal properties
        internal Inventor.AssemblyDocument InternalAssemblyDocument { get; set; }
        #endregion

        #region Private constructors
        private InventorAssemblyDocument(Inventor.AssemblyDocument assemblyDocument)
        {
            InternalAssemblyDocument = assemblyDocument;
        }     
    
        private InventorAssemblyDocument(InventorAssemblyDocument assemblyDocument)
        {
            InternalAssemblyDocument = assemblyDocument.InternalAssemblyDocument;
        }
        #endregion

        #region Private mutators
        private void InternalActivate()
        {
            AssemblyDocumentInstance.Activate();
        }

        private void InternalClose()
        {
            AssemblyDocumentInstance.Close();
        }
        #endregion

        #region Public properties
        public Inventor.AssemblyDocument AssemblyDocumentInstance
        {
            get { return InternalAssemblyDocument; }
            set { InternalAssemblyDocument = value; }
        }
        #endregion

        #region Public static constructors
        public static InventorAssemblyDocument ByAssemblyDocument(InventorAssemblyDocument assemblyDocument)
        {
            return new InventorAssemblyDocument(assemblyDocument);
        }

        public static InventorAssemblyDocument ByAssemblyDocument(Inventor.AssemblyDocument assemblyDocument)
        {
            return new InventorAssemblyDocument(assemblyDocument);
        }
        #endregion

        #region Public methods
        public void Activate()
        {
            InternalActivate();
        }



        public void Close()
        {
            InternalClose();
        }   
        #endregion

        #region Internal static constructors

        #endregion

        #region Tesselation

        #endregion


    }
}
