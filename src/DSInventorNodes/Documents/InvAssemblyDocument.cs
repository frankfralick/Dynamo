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
    public class InvAssemblyDocument
    {

        #region Internal properties
        internal Inventor.AssemblyDocument InternalAssemblyDocument { get; set; }
        #endregion

        #region Private constructors
        private InvAssemblyDocument(Inventor.AssemblyDocument assemblyDocument)
        {
            InternalAssemblyDocument = assemblyDocument;
        }     
    
        private InvAssemblyDocument(InvAssemblyDocument assemblyDocument)
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
        public static InvAssemblyDocument ByAssemblyDocument(InvAssemblyDocument assemblyDocument)
        {
            return new InvAssemblyDocument(assemblyDocument);
        }

        public static InvAssemblyDocument ByAssemblyDocument(Inventor.AssemblyDocument assemblyDocument)
        {
            return new InvAssemblyDocument(assemblyDocument);
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
