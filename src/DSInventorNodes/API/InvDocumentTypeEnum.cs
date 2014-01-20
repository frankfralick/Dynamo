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

        internal DocumentTypeEnum InternalkUnknownDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kUnknownDocumentObject; }
        }

        internal DocumentTypeEnum InternalkPartDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kPartDocumentObject; }
        }

        internal DocumentTypeEnum InternalkAssemblyDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kAssemblyDocumentObject; }
        }

        internal DocumentTypeEnum InternalkDrawingDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kDrawingDocumentObject; }
        }

        internal DocumentTypeEnum InternalkPresentationDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kPresentationDocumentObject; }
        }

        internal DocumentTypeEnum InternalkDesignElementDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kDesignElementDocumentObject; }
        }

        internal DocumentTypeEnum InternalkForeignModelDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kForeignModelDocumentObject; }
        }

        internal DocumentTypeEnum InternalkSATFileDocumentObject
        {
            get { return Inventor.DocumentTypeEnum.kSATFileDocumentObject; }
        }

        internal DocumentTypeEnum InternalkNoDocument
        {
            get { return Inventor.DocumentTypeEnum.kNoDocument; }
        }

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

        public DocumentTypeEnum kUnknownDocumentObject
        {
            get { return InternalkUnknownDocumentObject; }
        }

        public DocumentTypeEnum kPartDocumentObject
        {
            get { return InternalkPartDocumentObject; }
        }

        public DocumentTypeEnum kAssemblyDocumentObject
        {
            get { return InternalkAssemblyDocumentObject; }
        }

        public DocumentTypeEnum kDrawingDocumentObject
        {
            get { return InternalkDrawingDocumentObject; }
        }

        public DocumentTypeEnum kPresentationDocumentObject
        {
            get { return InternalkPresentationDocumentObject; }
        }

        public DocumentTypeEnum kDesignElementDocumentObject
        {
            get { return InternalkDesignElementDocumentObject; }
        }

        public DocumentTypeEnum kForeignModelDocumentObject
        {
            get { return InternalkForeignModelDocumentObject; }
        }

        public DocumentTypeEnum kSATFileDocumentObject
        {
            get { return InternalkSATFileDocumentObject; }
        }

        public DocumentTypeEnum kNoDocument
        {
            get { return InternalkNoDocument; }
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
