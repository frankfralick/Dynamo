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
    public class InvPartDocument
    {

        #region Internal properties
        internal Inventor.PartDocument InternalPartDocument { get; set; }
        #endregion

        #region Private constructors
        private InvPartDocument(Inventor.PartDocument partDocument)
        {
            InternalPartDocument = partDocument;
        }

        private InvPartDocument(InvPartDocument partDocument)
        {
            InternalPartDocument = partDocument.InternalPartDocument;
        }
        #endregion

        #region Private mutators
        private void InternalActivate()
        {
            PartDocumentInstance.Activate();
        }

        private void InternalClose()
        {
            PartDocumentInstance.Close();
        }
        #endregion

        #region Public properties
        public Inventor.PartDocument PartDocumentInstance
        {
            get { return InternalPartDocument; }
            set { InternalPartDocument = value; }
        }
        #endregion

        #region Public static constructors
        public static InvPartDocument ByPartDocument(InvPartDocument partDocument)
        {
            return new InvPartDocument(partDocument);
        }

        public static InvPartDocument ByPartDocument(Inventor.PartDocument partDocument)
        {
            return new InvPartDocument(partDocument);
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
    }
}
