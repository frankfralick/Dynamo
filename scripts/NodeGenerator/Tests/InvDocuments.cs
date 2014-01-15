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
    class InvDocuments
    {
        #region Internal properties
        internal int InternalCount { get; }

        internal _Document InternalItem { get; }

        internal _Document InternalItemByName { get; }

        internal int InternalLoadedCount { get; }

        internal objectTypeEnum InternalType { get; }

        internal DocumentsEnumerator InternalVisibleDocuments { get; }


        #region Private constructors
        private InvDocuments(Inventor.Documents documents)
        {
            InternalDocuments = documents;
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
        #endregion

        #region Public static constructors
        public static InvDocuments ByInvDocuments(InvDocuments invDocuments)
        {
            return new InvDocuments(invDocuments)
        }
        #endregion

        #region Public methods
        #endregion
    }
}
