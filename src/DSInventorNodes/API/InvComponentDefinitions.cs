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
    public class InvComponentDefinitions
    {
        #region Internal properties
        internal Inventor.ComponentDefinitions InternalComponentDefinitions { get; set; }

        internal int InternalCount
        {
            get { return ComponentDefinitionsInstance.Count; }
        }

        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(ComponentDefinitionsInstance.Type); }
        }


        #endregion

        #region Private constructors
        private InvComponentDefinitions(InvComponentDefinitions invComponentDefinitions)
        {
            InternalComponentDefinitions = invComponentDefinitions.InternalComponentDefinitions;
        }

        private InvComponentDefinitions(Inventor.ComponentDefinitions invComponentDefinitions)
        {
            InternalComponentDefinitions = invComponentDefinitions;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.ComponentDefinitions ComponentDefinitionsInstance
        {
            get { return InternalComponentDefinitions; }
            set { InternalComponentDefinitions = value; }
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
        public static InvComponentDefinitions ByInvComponentDefinitions(InvComponentDefinitions invComponentDefinitions)
        {
            return new InvComponentDefinitions(invComponentDefinitions);
        }
        public static InvComponentDefinitions ByInvComponentDefinitions(Inventor.ComponentDefinitions invComponentDefinitions)
        {
            return new InvComponentDefinitions(invComponentDefinitions);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
