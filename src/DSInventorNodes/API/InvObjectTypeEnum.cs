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
    public class InvObjectTypeEnum
    {
        #region Internal properties
        internal Inventor.ObjectTypeEnum InternalObjectTypeEnum { get; set; }

        #endregion

        //internal ObjectTypeEnum InternalkNoOwnership
        //{
        //    get { return Inventor.ObjectTypeEnum.kNoOwnership; }
        //}
        //internal ObjectTypeEnum InternalkSaveOwnership
        //{
        //    get { return Inventor.ObjectTypeEnum.kSaveOwnership; }
        //}
        //internal ObjectTypeEnum InternalkExclusiveOwnership
        //{
        //    get { return Inventor.ObjectTypeEnum.kExclusiveOwnership; }
        //}
        #region Private constructors
        private InvObjectTypeEnum(InvObjectTypeEnum invObjectTypeEnum)
        {
            InternalObjectTypeEnum = invObjectTypeEnum.InternalObjectTypeEnum;
        }

        private InvObjectTypeEnum(Inventor.ObjectTypeEnum invObjectTypeEnum)
        {
            InternalObjectTypeEnum = invObjectTypeEnum;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.ObjectTypeEnum ObjectTypeEnumInstance
        {
            get { return InternalObjectTypeEnum; }
            set { InternalObjectTypeEnum = value; }
        }

        #endregion
        //public ObjectTypeEnum kNoOwnership
        //{
        //    get { return InternalkNoOwnership; }
        //}
        //public ObjectTypeEnum kSaveOwnership
        //{
        //    get { return InternalkSaveOwnership; }
        //}
        //public ObjectTypeEnum kExclusiveOwnership
        //{
        //    get { return InternalkExclusiveOwnership; }
        //}
        #region Public static constructors
        public static InvObjectTypeEnum ByInvObjectTypeEnum(InvObjectTypeEnum invObjectTypeEnum)
        {
            return new InvObjectTypeEnum(invObjectTypeEnum);
        }
        public static InvObjectTypeEnum ByInvObjectTypeEnum(Inventor.ObjectTypeEnum invObjectTypeEnum)
        {
            return new InvObjectTypeEnum(invObjectTypeEnum);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
