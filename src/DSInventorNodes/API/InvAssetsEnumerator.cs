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
    public class InvAssetsEnumerator
    {
        #region Internal properties
        internal Inventor.AssetsEnumerator InternalAssetsEnumerator { get; set; }

        internal Object InternalApplication
        {
            get { return AssetsEnumeratorInstance.Application; }
        }

        internal int InternalCount
        {
            get { return AssetsEnumeratorInstance.Count; }
        }

        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(AssetsEnumeratorInstance.Type); }
        }


        #endregion

        #region Private constructors
        private InvAssetsEnumerator(InvAssetsEnumerator invAssetsEnumerator)
        {
            InternalAssetsEnumerator = invAssetsEnumerator.InternalAssetsEnumerator;
        }

        private InvAssetsEnumerator(Inventor.AssetsEnumerator invAssetsEnumerator)
        {
            InternalAssetsEnumerator = invAssetsEnumerator;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.AssetsEnumerator AssetsEnumeratorInstance
        {
            get { return InternalAssetsEnumerator; }
            set { InternalAssetsEnumerator = value; }
        }

        public Object Application
        {
            get { return InternalApplication; }
        }

        public int Count
        {
            get { return InternalCount; }
        }

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        #endregion
        #region Public static constructors
        public static InvAssetsEnumerator ByInvAssetsEnumerator(InvAssetsEnumerator invAssetsEnumerator)
        {
            return new InvAssetsEnumerator(invAssetsEnumerator);
        }
        public static InvAssetsEnumerator ByInvAssetsEnumerator(Inventor.AssetsEnumerator invAssetsEnumerator)
        {
            return new InvAssetsEnumerator(invAssetsEnumerator);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
