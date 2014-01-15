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
    class InvCameraEventsClass
    {
        #region Internal properties
        internal object InternalApplication { get; }

        internal object InternalParent { get; }

        internal objectTypeEnum InternalType { get; }


        #region Private constructors
        private InvCameraEventsClass(Inventor.CameraEventsClass cameraEventsClass)
        {
            InternalCameraEventsClass = cameraEventsClass;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.CameraEventsClass CameraEventsClassInstance
        {
            get { return InternalCameraEventsClass; }
            set { InternalCameraEventsClass = value; }
        }
        #endregion

        #region Public static constructors
        public static InvCameraEventsClass ByInvCameraEventsClass(InvCameraEventsClass invCameraEventsClass)
        {
            return new InvCameraEventsClass(invCameraEventsClass)
        }
        #endregion

        #region Public methods
        #endregion
    }
}
