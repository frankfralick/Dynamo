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
    public class InvAssemblyComponentDefinitions
    {
        #region Internal properties
        internal Inventor.AssemblyComponentDefinitions InternalAssemblyComponentDefinitions { get; set; }

        internal Object InternalApplication
        {
            get { return AssemblyComponentDefinitionsInstance.Application; }
        }

        internal int InternalCount
        {
            get { return AssemblyComponentDefinitionsInstance.Count; }
        }

        //internal Inv_AssemblyDocument InternalParent
        //{
        //    get { return Inv_AssemblyDocument.ByInv_AssemblyDocument(AssemblyComponentDefinitionsInstance.Parent); }
        //}


        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(AssemblyComponentDefinitionsInstance.Type); }
        }


        #endregion

        #region Private constructors
        private InvAssemblyComponentDefinitions(InvAssemblyComponentDefinitions invAssemblyComponentDefinitions)
        {
            InternalAssemblyComponentDefinitions = invAssemblyComponentDefinitions.InternalAssemblyComponentDefinitions;
        }

        private InvAssemblyComponentDefinitions(Inventor.AssemblyComponentDefinitions invAssemblyComponentDefinitions)
        {
            InternalAssemblyComponentDefinitions = invAssemblyComponentDefinitions;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.AssemblyComponentDefinitions AssemblyComponentDefinitionsInstance
        {
            get { return InternalAssemblyComponentDefinitions; }
            set { InternalAssemblyComponentDefinitions = value; }
        }

        public Object Application
        {
            get { return InternalApplication; }
        }

        public int Count
        {
            get { return InternalCount; }
        }

        //public Inv_AssemblyDocument Parent
        //{
        //    get { return InternalParent; }
        //}

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        #endregion
        #region Public static constructors
        public static InvAssemblyComponentDefinitions ByInvAssemblyComponentDefinitions(InvAssemblyComponentDefinitions invAssemblyComponentDefinitions)
        {
            return new InvAssemblyComponentDefinitions(invAssemblyComponentDefinitions);
        }
        public static InvAssemblyComponentDefinitions ByInvAssemblyComponentDefinitions(Inventor.AssemblyComponentDefinitions invAssemblyComponentDefinitions)
        {
            return new InvAssemblyComponentDefinitions(invAssemblyComponentDefinitions);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
