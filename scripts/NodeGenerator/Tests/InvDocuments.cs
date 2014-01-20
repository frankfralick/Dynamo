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

namespace DSInventorNodes
{
    [RegisterForTrace]
    public class InvDocuments
    {
        #region Internal properties
        internal Inventor.Documents InternalDocuments { get; set; }

        internal int InternalCount
        {
            get { return DocumentsInstance.Count; }
        }

        internal int InternalLoadedCount
        {
            get { return DocumentsInstance.LoadedCount; }
        }

        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(DocumentsInstance.Type); }
        }


        internal InvDocumentsEnumerator InternalVisibleDocuments
        {
            get { return InvDocumentsEnumerator.ByInvDocumentsEnumerator(DocumentsInstance.VisibleDocuments); }
        }


        #endregion

        #region Private constructors
        private InvDocuments(InvDocuments invDocuments)
        {
            InternalDocuments = invDocuments.InternalDocuments;
        }

        private InvDocuments(Inventor.Documents invDocuments)
        {
            InternalDocuments = invDocuments;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.Documents DocumentsInstance
        {
            get { return InternalDocuments; }
            set { InternalDocuments = value; }
        }

        public int Count
        {
            get { return InternalCount; }
        }

        public int LoadedCount
        {
            get { return InternalLoadedCount; }
        }

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        public InvDocumentsEnumerator VisibleDocuments
        {
            get { return InternalVisibleDocuments; }
        }

        #endregion

        #region Public static constructors
        public static InvDocuments ByInvDocuments(InvDocuments invDocuments)
        {
            return new InvDocuments(invDocuments);
        }
        public static InvDocuments ByInvDocuments(Inventor.Documents invDocuments)
        {
            return new InvDocuments(invDocuments);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
