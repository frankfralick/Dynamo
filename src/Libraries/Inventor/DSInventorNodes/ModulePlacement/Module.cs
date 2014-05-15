using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Runtime;
using Dynamo.Models;
using Dynamo.Utilities;
using InventorLibrary.API;
using InventorLibrary.GeometryConversion;
using InventorServices.Persistence;
using InventorServices.Utilities;
using Point = Autodesk.DesignScript.Geometry.Point;
using Application = Autodesk.DesignScript.Geometry.Application;

namespace InventorLibrary.ModulePlacement
{
    [IsVisibleInDynamoLibrary(false)]
    public class Module
    {
        #region Private fields
        private bool reuseDuplicates = true;
        private List<WorkPointProxy> layoutWorkPointProxies = new List<WorkPointProxy>();
        private List<WorkPoint> layoutWorkPoints = new List<WorkPoint>();
        #endregion

        #region Private constructors
        private Module(List<Point> points)
        {
            InternalModulePoints = points;
        }
        #endregion

        #region Internal methods
        internal Module InternalPlaceModule()
        {
            return this;
        }

        internal void PlaceWorkGeometryForContsraints(PartComponentDefinition layoutComponentDefinition, ComponentOccurrence layoutOccurrence, int moduleNumber)
        {
            PartDocument partDoc = (PartDocument)layoutComponentDefinition.Document;
            ReferenceKeyManager refKeyManager = partDoc.ReferenceKeyManager;
            ModuleId = moduleNumber;

            for (int i = 0; i < InternalModulePoints.Count; i++)
            {
                WorkPoint workPoint;
                //if (ReferenceKeyBinderModule.GetObjectFromTrace<Inventor.WorkPoint>(ModuleId, i, refKeyManager, out workPoint))
                //{
                //    Inventor.Point newLocation = InventorPersistenceManager.InventorApplication.TransientGeometry.CreatePoint(InternalModulePoints[i].X,
                //                                                                                                              InternalModulePoints[i].Y,
                //                                                                                                              InternalModulePoints[i].Z);

                //    workPoint.SetFixed(InternalModulePoints[i].ToPoint());
                //}

                //else
                //{
                //    workPoint = layoutComponentDefinition.WorkPoints.AddFixed(InternalModulePoints[i].ToPoint(), false);
                //    ReferenceKeyBinderModule.SetObjectForTrace<WorkPoint>(ModuleId, i, workPoint, ModuleUtilities.ReferenceKeysSorter);
                //}

                //workPoint.Visible = false;

                //object workPointProxyObject;
                //layoutOccurrence.CreateGeometryProxy(workPoint, out workPointProxyObject);
                //LayoutWorkPointProxies.Add((WorkPointProxy)workPointProxyObject);
                //LayoutWorkPoints.Add(workPoint);
            }

            //If we will have more than 2 constraints, it will help assembly stability later
            //if we have a plane to constrain to first.
            if (InternalModulePoints.Count > 2)
            {
                
                WorkPlane workPlane;
                //if (ReferenceKeyBinderModule.GetObjectFromTrace<Inventor.WorkPlane>(ModuleId, InternalModulePoints.Count, refKeyManager, out workPlane))
                //{
                //    if (workPlane.DefinitionType == WorkPlaneDefinitionEnum.kThreePointsWorkPlane)
                //    {
                //        workPlane.SetByThreePoints(LayoutWorkPoints[0], LayoutWorkPoints[1], LayoutWorkPoints[2]);
                //    }
                //}
                //else
                //{
                //    //If the first three points are colinear, adding a workplane will fail.  We will check the area of a triangle 
                //    //described by the first three points. If the area is very close to 0, we can assume these points are colinear, and we should
                //    //not attempt to construct a work plane from them.
                //    Inventor.Point pt1 = LayoutWorkPoints[0].Point;
                //    Inventor.Point pt2 = LayoutWorkPoints[1].Point;
                //    Inventor.Point pt3 = LayoutWorkPoints[2].Point;
                //    if (Math.Abs(pt1.X * (pt2.Y - pt3.Y) + pt2.X * (pt3.Y - pt1.Y) + pt3.X * (pt1.Y - pt2.Y)) > .0000001)
                //    {
                //        workPlane = layoutComponentDefinition.WorkPlanes.AddByThreePoints(LayoutWorkPoints[0], LayoutWorkPoints[1], LayoutWorkPoints[2], false);
                //        ReferenceKeyBinderModule.SetObjectForTrace<WorkPlane>(ModuleId, InternalModulePoints.Count, workPlane, ModuleUtilities.ReferenceKeysSorter);
                //        workPlane.Grounded = true;
                //        //workPlane.Visible = false;
                //        LayoutWorkPlane = workPlane;
                //        object wPlaneProxyObject;
                //        layoutOccurrence.CreateGeometryProxy(workPlane, out wPlaneProxyObject);
                //        ModuleWorkPlaneProxyAssembly = (WorkPlaneProxy)wPlaneProxyObject; 
                //    }
                    
                //}  
            }
        }

        internal void MakeInvCopy(string templateAssemblyPath,
                         string templateDrawingPath,
                         string targetDirectory,
                         OccurrenceList occurrenceList,
                         int count,
                         UniqueModuleEvaluator uniqueModuleEvaluator)
        {
            // TODO Test for the existance of folders and assemblies.
            int moduleId = count;
            string topFileFullName;
            string targetPath = targetDirectory;
            TemplateAssemblyPath = templateAssemblyPath;
            TemplateDrawingPath = templateDrawingPath;
            
            UniqueModules = uniqueModuleEvaluator;

            
            //Get the folder name that will be used to store the files associated with this Module.
            string folderName = GetModuleFolderPath();

            //Need to get number of the parent occ, top level name as foldername
            string pathString = System.IO.Path.Combine(targetPath, folderName);

            topFileFullName = occurrenceList.TargetAssembly.FullDocumentName;
            string topFileNameOnly = System.IO.Path.GetFileName(topFileFullName);
            ModulePath = System.IO.Path.Combine(pathString, topFileNameOnly);


            TupleList<string, string> filePathPair = new TupleList<string, string>();

            for (int i = 0; i < occurrenceList.Items.Count; i++)
            {
                string targetOccPath = occurrenceList.Items[i].ReferencedFileDescriptor.FullFileName;
                string newCopyName = System.IO.Path.GetFileName(targetOccPath);
                string newFullCopyName = System.IO.Path.Combine(pathString, newCopyName);
                filePathPair.Add(targetOccPath, newFullCopyName);
            }

            //Check if an earlier module already made the folder, if not, create it.
            if (!System.IO.Directory.Exists(pathString))
            {
                //firstTime = true;
                System.IO.Directory.CreateDirectory(pathString);
                //AssemblyReplaceRef(oAppServ, oOccs.TargetAssembly, filePathPair, pathString);
                //ApprenticeServerDocument oAssDoc;
                AssemblyDocument oAssDoc = (AssemblyDocument)PersistenceManager.InventorApplication.Documents.Open(TemplateAssemblyPath, false);
                
                
                //Fuck why can't I use Apprentice Server at the same time I'm using Inventor! 
                //FileSaveAs fileSaver;
                //fileSaver = oAppServ.FileSaveAs;
                //fileSaver.AddFileToSave(oAssDoc, ModulePath);
                //fileSaver.ExecuteSaveCopyAs();
                oAssDoc.SaveAs(ModulePath, true);
                oAssDoc.Close(true);


                //Need to copy presentation files if there are any.  For now this is only going to work with the top assembly.
                string templateDirectory = System.IO.Path.GetDirectoryName(TemplateAssemblyPath);
                string[] presentationFiles = System.IO.Directory.GetFiles(templateDirectory, "*.ipn");
                //If we want the ability to have subassemblies with .ipn files or multiple ones, this will have to be changed
                //to iterate over all the .ipn files.
                if (presentationFiles.Length != 0)
                {
                    string newCopyPresName = System.IO.Path.GetFileName(presentationFiles[0]);
                    string newFullCopyPresName = System.IO.Path.Combine(pathString, newCopyPresName);

                    //ApprenticeServerDocument presentationDocument = oAppServ.Open(presentationFiles[0]);
                    PresentationDocument presentationDocument = (PresentationDocument)PersistenceManager.InventorApplication.Documents.Open(presentationFiles[0], false);
                    DocumentDescriptorsEnumerator presFileDescriptors = presentationDocument.ReferencedDocumentDescriptors;
                    foreach (DocumentDescriptor refPresDocDescriptor in presFileDescriptors)
                    {
                        if (refPresDocDescriptor.FullDocumentName == TemplateAssemblyPath)
                        {
                            refPresDocDescriptor.ReferencedFileDescriptor.ReplaceReference(ModulePath);
                            //FileSaveAs fileSavePres;
                            //fileSavePres = oAppServ.FileSaveAs;
                            //fileSavePres.AddFileToSave(presentationDocument, newFullCopyPresName);

                            presentationDocument.SaveAs(newFullCopyPresName, true);
                            presentationDocument.Close(true);
                        }
                    }
                }

                string newCopyDrawingName = System.IO.Path.GetFileName(TemplateDrawingPath);
                string newFullCopyDrawingName = System.IO.Path.Combine(pathString, newCopyDrawingName);

                if (TemplateDrawingPath != "")
                {
                    DrawingDocument drawingDoc = (DrawingDocument)PersistenceManager.InventorApplication.Documents.Open(TemplateDrawingPath, false);
                    DocumentDescriptorsEnumerator drawingFileDescriptors = drawingDoc.ReferencedDocumentDescriptors;
                    //This needs to be fixed.  It was written with the assumption that only the template assembly would be in 
                    //the details and be first in the collection of document descriptors.  This was a safe assumption when
                    //I was the only user of this code. Need to iterate through drawingFileDescriptors and match names 
                    //and replace correct references.  Possibly can use the "filePathPair" object for name 
                    //matching/reference replacing.
                    //drawingFileDescriptors[1].ReferencedFileDescriptor.ReplaceReference(topAssemblyNewLocation);
                    foreach (DocumentDescriptor refDocDescriptor in drawingFileDescriptors)
                    {
                        foreach (Tuple<string, string> pathPair in filePathPair)
                        {
                            string newFileNameLower = System.IO.Path.GetFileName(pathPair.Item2);
                            string drawingReferenceLower = System.IO.Path.GetFileName(refDocDescriptor.FullDocumentName);
                            string topAssemblyLower = System.IO.Path.GetFileName(ModulePath);
                            if (topAssemblyLower == drawingReferenceLower)
                            {
                                refDocDescriptor.ReferencedFileDescriptor.ReplaceReference(ModulePath);
                            }
                            if (newFileNameLower == drawingReferenceLower)
                            {
                                refDocDescriptor.ReferencedFileDescriptor.ReplaceReference(pathPair.Item2);
                            }
                        }
                    }

                    //FileSaveAs fileSaveDrawing;
                    //fileSaveDrawing = oAppServ.FileSaveAs;
                    //fileSaveDrawing.AddFileToSave(drawingDoc, newFullCopyDrawingName);
                    //fileSaveDrawing.ExecuteSaveCopyAs();

                    drawingDoc.SaveAs(newFullCopyDrawingName, true);

                    //firstTime = true;

                    if (!UniqueModules.DetailDocumentPaths.Contains(newFullCopyDrawingName))
                    {
                        UniqueModules.DetailDocumentPaths.Add(newFullCopyDrawingName);
                    }
                }
            }
        }

        private string GetModuleFolderPath()
        {
            string folderName;
            string geoMapString = System.Convert.ToString(GeometryMapIndex);
            if (ReuseDuplicates == false)
            {
                if (GeometryMapIndex < 10)
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " 00" + geoMapString;
                }

                else if (10 <= GeometryMapIndex && GeometryMapIndex < 100)
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " 0" + geoMapString;
                }
                else
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " " + geoMapString;
                }
            }

            else
            {
                string moduleIdString = System.Convert.ToString(ModuleId);
                if (ModuleId < 10)
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " 00" + moduleIdString;
                }
                else if (10 <= ModuleId && ModuleId < 100)
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " 0" + moduleIdString;
                }
                else
                {
                    folderName = System.IO.Path.GetFileNameWithoutExtension(TemplateAssemblyPath) + " " + moduleIdString;
                }
            }
            return folderName;
        }
        #endregion

        #region Internal properties


        internal int GeometryMapIndex { get; set; }

        internal List<Point> InternalModulePoints { get; set; }

        internal WorkPlane LayoutWorkPlane { get; set; }

        internal List<WorkPointProxy> LayoutWorkPointProxies
        {
            get { return layoutWorkPointProxies; }
            set { layoutWorkPointProxies = value; }
        }

        internal List<WorkPoint> LayoutWorkPoints
        {
            get { return layoutWorkPoints; }
            set { layoutWorkPoints = value; }
        }

        internal int ModuleId { get; set; }

        internal string ModulePath { get; set; }

        internal WorkPlaneProxy ModuleWorkPlaneProxyAssembly { get; set; }

        internal bool ReuseDuplicates
        {
            get { return reuseDuplicates; }
            set { reuseDuplicates = value; }
        }

        internal string TemplateAssemblyPath { get; set; }

        internal string TemplateDrawingPath { get; set; }

        internal UniqueModuleEvaluator UniqueModules { get; set; }
        #endregion

        #region Public static constructors
        public static Module ByPoints(List<Point> points)
        {
            return new Module(points);
        }
        #endregion

        #region Public methods
        public Module PlaceModule()
        {
            return InternalPlaceModule();
        }
        #endregion
    }
}
