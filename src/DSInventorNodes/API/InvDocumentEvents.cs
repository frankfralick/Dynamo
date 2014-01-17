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
    public class InvDocumentEvents
    {
        #region Internal properties
        internal Inventor.DocumentEvents InternalDocumentEvents { get; set; }

        #endregion

        #region Private constructors
        private InvDocumentEvents(InvDocumentEvents invDocumentEvents)
        {
            InternalDocumentEvents = invDocumentEvents.InternalDocumentEvents;
        }

        private InvDocumentEvents(Inventor.DocumentEvents invDocumentEvents)
        {
            InternalDocumentEvents = invDocumentEvents;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.DocumentEvents DocumentEventsInstance
        {
            get { return InternalDocumentEvents; }
            set { InternalDocumentEvents = value; }
        }

        #endregion
        #region Public static constructors
        public static InvDocumentEvents ByInvDocumentEvents(InvDocumentEvents invDocumentEvents)
        {
            return new InvDocumentEvents(invDocumentEvents);
        }
        public static InvDocumentEvents ByInvDocumentEvents(Inventor.DocumentEvents invDocumentEvents)
        {
            return new InvDocumentEvents(invDocumentEvents);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
