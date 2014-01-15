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

        #endregion

        #region Private constructors
        #endregion

        #region Private mutators
        #endregion

        #region Public properties
        #endregion

        #region Public static constructors
        public static InvDocuments ByInvDocuments(InvDocuments invDocuments)
        {
        }
        #endregion

        #region Public methods
        #endregion
    }
}
