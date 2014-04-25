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

        #region Internal properties
        internal string InternalTemplateAssemblyPath { get; set; }
        internal string InternalDestinationFolder { get; set; }
        #endregion

        #region Private constructors
        private Modules(List<Module> modules)
        {
            modulesList = new List<Module>();
            foreach (Module module in modules)
            {
                modulesList.Add(module);
            }
        }

        private Modules(List<List<Point>> pointsList)
        {
            modulesList = new List<Module>();
            foreach (var points in pointsList)
            {
                modulesList.Add(Module.ByPoints(points));
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Method checks that both the template assembly, and the folder specified as the destination
        /// for new files is within the active project.  
        /// </summary>
        /// <returns></returns>
        private bool CompatibleWithActiveProject()
        {
            return true;
        }

        /// <summary>
        /// We are creating a new assembly, with new unique subassemblies derived from the template assembly.
        /// The first thing we need in this new assembly is a "layout part".  This will be the first thing we put
        /// in the assembly and it will contain all of the work points that we need to place and contstrain 
        /// each module instance.
        /// </summary>
        private void CreateLayout(string destinationFolder)
        {
            CreateLayoutPartFile();  
        }

        private void CreateLayoutPartFile()
        {
        }

        private void InternalPlaceModules(string templateAssemblyPath, string destinationFolder)
        {
            InternalTemplateAssemblyPath = templateAssemblyPath;
            InternalDestinationFolder = destinationFolder;
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

        //Need to decide what the workflow should be.  Module(Points) fed a List<List<Point>> in the UI could feed its output
        //to this constructor.  May need to have a constructor on this class that takes a List<List<Point>> to keep track
        //of everything.
        #region Public static constructors
        public static Modules ByModules(List<Module> modules)
        {
            return new Modules(modules);
        }

        public static Modules ByPointsList(List<List<Point>> points)
        {
            return new Modules(points);
        }
        #endregion

        #region Public methods
        public void Add(Module module)
        {
            modulesList.Add(module);
        }

        public Modules EvaluateUniqueModules()
        {
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateAssemblyPath"></param>
        /// <param name="destinationFolder"></param>
        /// <returns></returns>
        public Modules PlaceModules(string templateAssemblyPath, string destinationFolder)
        {
            InternalPlaceModules(templateAssemblyPath, destinationFolder);
            return this;
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
