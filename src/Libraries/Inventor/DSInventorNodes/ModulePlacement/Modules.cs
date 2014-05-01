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
using InventorLibrary.GeometryConversion;
using InventorServices.Persistence;
using InventorServices.Utilities;
using InventorLibrary.API;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace InventorLibrary.ModulePlacement
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
            //TODO: The next line is super brittle.  Need to finish writing out what expected behavior should be for responding to 
            //Document activations/deactivations.  Right now InventorPersistenceManager.ActiveAssemblyDoc is being kept current, but that
            //may be bad.
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

        private void EnsureLayoutPartExists(string destinationFolder)
        {
            //TODO: Add overload that lets user set this partTemplateFile.  This is a template in the sense of 
            //what the user sees in the UI when creating a new file.
            string partTemplateFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2014\Templates\Standard.ipt";
            LayoutPartPath = destinationFolder + "\\Layout.ipt";
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
                //We should alert the user that this part exists and ask them what they want to do.
                //Option A: Use the existing file.  Create work geometry in it, even if there is other work geometry
                //          already there.  Maybe they want to add to something they already did.
                //Option B: Delete the file and recreate it.

                //TODO: Are the Revit guys using Forms?
            }
        }

        private void InternalPlaceModules(string templateAssemblyPath, string destinationFolder)
        {
            //Do some initial validation that this is going to work.

            //Is the constraint set (Points) uniform?
            if (!IsConstraintSetUniform)
            {
                throw new Exception("Each module must have the same number of points.");
            }

            //Create a layout file.  This file will contain all the individual geometries as 
            //work geometry.  It will be placed first in the assembly we are making, and each
            //Module will get constrained to its corresponding set of work geometry.
            //This only needs to happen the first time.
            EnsureLayoutPartExists(destinationFolder);

            //Place the layout part and put work geometry in it.
            CreateLayout(destinationFolder);


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
                ReferenceKeyBinder.SetObjectForTrace(LayoutOccurrence);
                PartComponentDefinition layoutComponentDefinition = (PartComponentDefinition)LayoutOccurrence.Definition;
                AssemblyOccurrences = componentDefinition.Occurrences;
                return layoutComponentDefinition;
            }
        }
        #endregion

        #region Internal properties
        internal int InternalCount
        {
            get { return modulesList.Count; }
        }

        private bool IsConstraintSetUniform
        {
            get
            {
                int contstraintCount = ModulesList[0].InternalModulePoints.Count;
                if (ModulesList.Any(p => contstraintCount != p.InternalModulePoints.Count))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
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
        ///Method to pair and place a set of generic module geometries with a real Inventor 
        ///assembly.
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

