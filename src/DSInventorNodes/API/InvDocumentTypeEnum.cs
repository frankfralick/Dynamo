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
    public class InvDocumentTypeEnum
    {
        #region Internal properties
        internal Inventor.DocumentTypeEnum InternalDocumentTypeEnum { get; set; }

        #endregion

        #region Private constructors
        private InvDocumentTypeEnum(InvDocumentTypeEnum invDocumentTypeEnum)
        {
            InternalDocumentTypeEnum = invDocumentTypeEnum.InternalDocumentTypeEnum;
        }

        private InvDocumentTypeEnum(Inventor.DocumentTypeEnum invDocumentTypeEnum)
        {
            InternalDocumentTypeEnum = invDocumentTypeEnum;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.DocumentTypeEnum DocumentTypeEnumInstance
        {
            get { return InternalDocumentTypeEnum; }
            set { InternalDocumentTypeEnum = value; }
        }

        #endregion
        #region Public static constructors
        public static InvDocumentTypeEnum ByInvDocumentTypeEnum(InvDocumentTypeEnum invDocumentTypeEnum)
        {
            return new InvDocumentTypeEnum(invDocumentTypeEnum);
        }
        public static InvDocumentTypeEnum ByInvDocumentTypeEnum(Inventor.DocumentTypeEnum invDocumentTypeEnum)
        {
            return new InvDocumentTypeEnum(invDocumentTypeEnum);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
