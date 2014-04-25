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
using DSInventorNodes.API;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace DSInventorNodes.ModulePlacement
{
    [RegisterForTrace]
    public class Modules : IEnumerable<Module>
    {
        #region Private fields
        private List<Module> modulesList;
        #endregion

        #region Internal properties
        internal ComponentOccurrences AssemblyOccurrences { get; set; }
        internal string DestinationFolder { get; set; }
        internal ComponentOccurrence LayoutOccurrence { get; set; }
        internal PartDocument LayoutPartDocument { get; set; }
        internal string LayoutPartPath { get; set; }
        internal string TemplateAssemblyPath { get; set; }
        internal Matrix TransformationMatrix { get; set; }
        
        internal List<Module> ModulesList
        {
            get { return modulesList; }
            set { modulesList = value; }
        }
        #endregion

        #region Private constructors
        private Modules(List<Module> modules)
        {
            modulesList = new List<Module>();
            foreach (Module module in modules)
            {
                ModulesList.Add(module);
            }
        }

        private Modules(List<List<Point>> pointsList)
        {
            modulesList = new List<Module>();
            foreach (var points in pointsList)
            {
                ModulesList.Add(Module.ByPoints(points));
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
            //TODO: Flesh this out
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
            Inventor.AssemblyComponentDefinition componentDefinition = InventorPersistenceManager.ActiveAssemblyDoc.ComponentDefinition;
            if (TransformationMatrix == null)
            {
                TransformationMatrix = InventorPersistenceManager.InventorApplication.TransientGeometry.CreateMatrix();
            }
            //We know that there is a Layout.ipt file on the disc if we are here.  
            //Try to bind to the ComponentOccurrence that corresponds to this file in
            //our assembly, otherwise place it, and SetObjectForTrace.
            PartComponentDefinition layoutCompDef = GetLayoutCompDef(componentDefinition);
        }

        private PartComponentDefinition GetLayoutCompDef(Inventor.AssemblyComponentDefinition componentDefinition)
        {
            ComponentOccurrence layoutOccurrence;
            if (ReferenceKeyBinder.GetObjectFromTrace<ComponentOccurrence>(out layoutOccurrence))
            {
                LayoutOccurrence = layoutOccurrence;
                PartComponentDefinition layoutComponentDefinition = (PartComponentDefinition)LayoutOccurrence.Definition;
                AssemblyOccurrences = componentDefinition.Occurrences;
                return layoutComponentDefinition;
            }
            else
            {
                LayoutOccurrence = componentDefinition.Occurrences.Add(LayoutPartPath, TransformationMatrix);
                ReferenceKeyBinder.SetObjectForTrace(layoutOccurrence);
                PartComponentDefinition layoutComponentDefinition = (PartComponentDefinition)LayoutOccurrence.Definition;
                AssemblyOccurrences = componentDefinition.Occurrences;
                return layoutComponentDefinition;
            }
        }

        private void InternalPlaceModules(string templateAssemblyPath, string destinationFolder)
        {
            TemplateAssemblyPath = templateAssemblyPath;
            DestinationFolder = destinationFolder;

            //Create a layout file.  This file will contain all the individual geometries as 
            //work geometry.  It will be placed first in the assembly we are making, and each
            //Module will get constrained to its corresponding set of work geometry.
            //This only needs to happen the first time.
            EnsureLayoutPartExists();


        }

        private void EnsureLayoutPartExists()
        {
            //TODO: Add overload that lets user set this partTemplateFile.  This is a template in the sense of 
            //what the user sees in the UI when creating a new file.
            string partTemplateFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2014\Templates\Standard.ipt";
            LayoutPartPath = DestinationFolder + "\\Layout.ipt";
            if (!System.IO.File.Exists(LayoutPartPath))
            {
                LayoutPartDocument = (PartDocument)InventorPersistenceManager.InventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject,
                                                                                                                        partTemplateFile,
                                                                                                                        true);
                LayoutPartDocument.SaveAs(LayoutPartPath, false);
                LayoutPartDocument.Close();
            }
            else
            {
                //If we are here something is fucked.  We created a Layout.ipt previously for this set of modules, but binding failed.  
                //At this point we could get that file and return it, which may be a mistake, or we could offer the user the choice to
                //overwrite that file, or create a new one of a different name?
                //For now, we will just open the old one and return it.
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
            return ModulesList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

