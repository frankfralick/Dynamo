using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using DSNodeServices;
using Dynamo.Models;
using Dynamo.Utilities;
using DSInventorNodes.GeometryObjects;
using DSInventorNodes.GeometryConversion;
using InventorServices.Persistence;
using InventorServices.Utilities;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace DSInventorNodes.ModulePlacement
{
    public class Modules : IEnumerable<Module>
    {
        #region Private fields
        private List<Module> modulesList;
        #endregion

        #region Private constructors
        public Modules(List<Module> modules)
        {
            modulesList = new List<Module>();
            foreach (Module module in modules)
            {
                modulesList.Add(module);
            }
        }
        #endregion

        #region Internal properties
        internal int InternalCount 
        {
            get { return modulesList.Count; } 
        }
        #endregion

        #region Public properties
        public int Count
        {
            get { return InternalCount; }
        }
        #endregion

        #region Public static constructors
        public static Modules ByModules(List<Module> modules)
        {
            return new Modules(modules);
        }
        #endregion

        #region Public methods
        public void Add(Module module)
        {
            modulesList.Add(module);
        }
        #endregion

        public IEnumerator<Module> GetEnumerator()
        {
            return modulesList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
