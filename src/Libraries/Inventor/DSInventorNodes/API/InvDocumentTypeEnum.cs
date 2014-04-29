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
using InventorLibrary.GeometryConversion;
using InventorServices.Persistence;

namespace InventorLibrary.API
{
    [RegisterForTrace]
    public enum InvDocumentTypeEnum
    {
        #region Enums
        kUnknownDocumentObject = Inventor.DocumentTypeEnum.kUnknownDocumentObject,

        kPartDocumentObject = Inventor.DocumentTypeEnum.kPartDocumentObject,

        kAssemblyDocumentObject = Inventor.DocumentTypeEnum.kAssemblyDocumentObject,

        kDrawingDocumentObject = Inventor.DocumentTypeEnum.kDrawingDocumentObject,

        kPresentationDocumentObject = Inventor.DocumentTypeEnum.kPresentationDocumentObject,

        kDesignElementDocumentObject = Inventor.DocumentTypeEnum.kDesignElementDocumentObject,

        kForeignModelDocumentObject = Inventor.DocumentTypeEnum.kForeignModelDocumentObject,
        
        kSATFileDocumentObject = Inventor.DocumentTypeEnum.kSATFileDocumentObject,

        kNoDocument = Inventor.DocumentTypeEnum.kNoDocument
        #endregion
    }
}
