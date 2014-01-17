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
    public class InvAttributeSets
    {
        #region Internal properties
        internal Inventor.AttributeSets InternalAttributeSets { get; set; }

        internal int InternalCount
        {
            get { return AttributeSetsInstance.Count; }
        }

        internal InvDataIO InternalDataIO
        {
            get { return InvDataIO.ByInvDataIO(AttributeSetsInstance.DataIO); }
        }


        internal Object InternalParent
        {
            get { return AttributeSetsInstance.Parent; }
        }

        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(AttributeSetsInstance.Type); }
        }


        #endregion

        #region Private constructors
        private InvAttributeSets(InvAttributeSets invAttributeSets)
        {
            InternalAttributeSets = invAttributeSets.InternalAttributeSets;
        }

        private InvAttributeSets(Inventor.AttributeSets invAttributeSets)
        {
            InternalAttributeSets = invAttributeSets;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.AttributeSets AttributeSetsInstance
        {
            get { return InternalAttributeSets; }
            set { InternalAttributeSets = value; }
        }

        public int Count
        {
            get { return InternalCount; }
        }

        public InvDataIO DataIO
        {
            get { return InternalDataIO; }
        }

        public Object Parent
        {
            get { return InternalParent; }
        }

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        #endregion
        #region Public static constructors
        public static InvAttributeSets ByInvAttributeSets(InvAttributeSets invAttributeSets)
        {
            return new InvAttributeSets(invAttributeSets);
        }
        public static InvAttributeSets ByInvAttributeSets(Inventor.AttributeSets invAttributeSets)
        {
            return new InvAttributeSets(invAttributeSets);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
