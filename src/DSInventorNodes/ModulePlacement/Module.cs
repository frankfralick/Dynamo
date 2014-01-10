using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
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


namespace DSInventorNodes.ModulePlacement
{
    //[Browsable(false)]
    [RegisterForTrace]
	public class Module
    {
        #region Internal properties    
        internal List<Inventor.Point> ModulePoints { get; set; }

        internal int GeometryMapIndex { get; set; }

        #endregion


        #region Private constructors

        private Module(List<Point> pointList)
        {
            ModulePoints = pointList.Select(p => p.ToPoint()).ToList();
        }

        #endregion


        #region Private mutators

        #endregion


        #region Public properties

        #endregion


        #region Public static constructors
        public static Module ByPoints(List<Point> points)
        {
            return new Module(points);
        }
        #endregion


        #region Internal static constructors

        #endregion

        //List<Inventor.Point> points;
        //string modulePath;
        //string targetAssemblyPath;
        //string targetDrawingPath;
        //List<WorkPoint> ModuleWorkPointListAssembly;
        //List<WorkPoint> ModuleWorkPointListTarget;
        //WorkPlane moduleWorkPlaneAssembly;
        //WorkPlane moduleWorkPlaneTarget;
        //List<WorkPointProxy> moduleWorkPointProxyAssembly;
        //List<WorkPointProxy> moduleWorkPointProxyTarget;
        //WorkPlaneProxy moduleWorkPlaneProxyAssembly;
        //WorkPlaneProxy moduleWorkPlaneProxyTarget;
        //PartComponentDefinition layoutCompDef;
        //AssemblyComponentDefinition frameCompDef;

        //Matrix oMatrix;
        //AssemblyComponentDefinition oCompDef;
        //TupleList<string, object> scopePairs = new TupleList<string, object>();
        //List<ComponentOccurrence> topOccSubsList = new List<ComponentOccurrence>();

        //bool firstTime = false;
        //bool createAllCopies;

        //int geometryMapIndex;

        

        //public List<Inventor.Point> Points { get; set; }

        //public List<Inventor.Point> ModulePoints
        //{
        //    get { return points; }
        //    set { points = value; }
        //}

        //public List<Inventor.Point> Points
        //{
        //    get{return points;}
        //    set{points = value;}
        //}

        //public WorkPlane ModuleWorkPlaneAssembly
        //{
        //    get{return moduleWorkPlaneAssembly;}
        //    set{moduleWorkPlaneAssembly = value;}
        //}

        //public PartComponentDefinition LayoutCompDef
        //{
        //    get { return layoutCompDef; }
        //    set { layoutCompDef = value; }
        //}

        //public AssemblyComponentDefinition FrameCompDef
        //{
        //    get { return frameCompDef; }
        //    set { frameCompDef = value; }
        //}

        ////The UniquePanels class could overwrite this value?
        //public string ModulePath
        //{
        //    get{return modulePath;}
        //    set{modulePath = value;}
        //}

        //public string TargetPath
        //{
        //    get { return targetAssemblyPath; }
        //    set { targetAssemblyPath = value; }
        //}

        //public string TargetDrawingPath
        //{
        //    get { return targetDrawingPath; }
        //    set { targetDrawingPath = value; }
        //}

        //public WorkPlane ModuleWorkPlaneTarget
        //{
        //    get{return moduleWorkPlaneTarget;}
        //    set{moduleWorkPlaneAssembly = value;}
        //}

        //public List<WorkPoint> ModuleWorkPointsAssembly
        //{
        //    get{return ModuleWorkPointListAssembly;}
        //    set{ModuleWorkPointListAssembly = value;}
        //}

        //public List<WorkPoint> ModuleWorkPointsTarget
        //{
        //    get{return ModuleWorkPointListTarget;}
        //    set{ModuleWorkPointListTarget = value;}
        //}

        //public List<WorkPointProxy> ModuleWorkPointsProxyAssembly
        //{
        //    get{return moduleWorkPointProxyAssembly;}
        //    set{moduleWorkPointProxyAssembly = value;}
        //}

        //public List<WorkPointProxy> ModuleWorkPointsProxyTarget
        //{
        //    get{return moduleWorkPointProxyTarget;}
        //    set{moduleWorkPointProxyTarget = value;}
        //}

        //public WorkPlaneProxy ModuleWorkPlaneProxyAssembly
        //{
        //    get{return moduleWorkPlaneProxyAssembly;}
        //    set{moduleWorkPlaneProxyAssembly = value;}
        //}

        //public WorkPlaneProxy ModuleWorkPlaneProxyTarget
        //{
        //    get{return moduleWorkPlaneProxyTarget;}
        //    set{moduleWorkPlaneProxyTarget = value;}
        //}

        //public bool FirstTime
        //{
        //    get { return firstTime; }
        //    set { firstTime = value; }
        //}

        //public bool CreateAllCopies
        //{
        //    get { return createAllCopies; }
        //    set { createAllCopies = value; }
        //}

        //public TupleList<string, object> ScopeNamePairs
        //{
        //    get { return scopePairs; }
        //    set { scopePairs = value; }
        //}

        //public List<ComponentOccurrence> TopOccurrences
        //{
        //    get{return topOccSubsList;}
        //    set{topOccSubsList = value;}
        //}

        //public void CreateInvLayout(Inventor.Application app,AssemblyComponentDefinition compDef)
        //{
        //    Inventor.Application oApp = app;
        //    //oApp = Button.InventorApplication;
        //    oCompDef = compDef;
        //    //this.oCompDef = oCompDef;
        //    ComponentOccurrences oOccCol;
        //    oOccCol = oCompDef.Occurrences;

        //    Matrix oMatrix;
        //    oMatrix = oApp.TransientGeometry.CreateMatrix();
        //    this.oMatrix = oMatrix;
        //    ComponentOccurrence oLayoutOcc;
        //    oLayoutOcc = oOccCol[1];

        //    PartComponentDefinition oLayoutCompDef;
        //    oLayoutCompDef = (PartComponentDefinition)oLayoutOcc.Definition;

        //    List<Inventor.Point> pointListLoc = new List<Inventor.Point>();
        //    List<Inventor.WorkPoint> workPointListAssembly = new List<Inventor.WorkPoint>();
        //    List<WorkPointProxy> workPointProxyListAssembly = new List<WorkPointProxy>();
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        Inventor.Point oVert;
        //        oVert = oApp.TransientGeometry.CreatePoint(System.Convert.ToDouble(Points[i].X), (double)Points[i].Y, (double)Points[i].Z);
        //        WorkPoint wp;
        //        wp = oLayoutCompDef.WorkPoints.AddFixed(oVert, false);
        //        wp.Grounded = true;
        //        wp.Visible = false;
        //        object wpProxyObject;
        //        WorkPointProxy wpProxy;
        //        oLayoutOcc.CreateGeometryProxy(wp, out wpProxyObject);
        //        wpProxy = (WorkPointProxy)wpProxyObject;
        //        workPointProxyListAssembly.Add(wpProxy);
        //        //PanelWorkPointListAssembly.Add(wp);
        //        workPointListAssembly.Add(wp);
        //        pointListLoc.Add(oVert);
        //    }

        //    ModuleWorkPointsProxyAssembly = workPointProxyListAssembly;
        //    ModuleWorkPointListAssembly = workPointListAssembly;
        //    WorkPlane assemblyPlane;
        //    //assemblyPlane = oLayoutCompDef.WorkPlanes.AddByThreePoints((pointListLoc[0].X,pointListLoc[0].Y,pointListLoc[0].Z), (pointListLoc[1].X,pointListLoc[1].Y,pointListLoc[1].Z), (pointListLoc[2].X,pointListLoc[2].Y,pointListLoc[2].Z));
        //    assemblyPlane = oLayoutCompDef.WorkPlanes.AddByThreePoints(workPointListAssembly[0], workPointListAssembly[1], workPointListAssembly[2]);
        //    ModuleWorkPlaneAssembly = assemblyPlane;
        //    assemblyPlane.Grounded = true;
        //    assemblyPlane.Visible = false;
        //    object wPlaneProxyObject;
        //    WorkPlaneProxy wPlaneProxy;
        //    oLayoutOcc.CreateGeometryProxy(assemblyPlane, out wPlaneProxyObject);
        //    wPlaneProxy = (WorkPlaneProxy)wPlaneProxyObject;
        //    ModuleWorkPlaneProxyAssembly = wPlaneProxy;
        //}

        ////public TupleList<string, double> OriginalParameters
        ////{
        ////    get
        ////    {
        ////        MMDB db = new MMDB();
        ////        TupleList<string, double> oldParams = db.Params(TargetPath);
        ////        return oldParams;
        ////    }
        ////}

        ////public void MakeInvCopy(ApprenticeServer appServ, string targetAssembly, string targetDrawing, string targetDirectory, OccurrenceList occList, int count, UniqueModule uniqueModulesCollection)
        ////{
        ////    //TODO Test for the existance of folders and assemblies.
        ////    ApprenticeServer oAppServ = appServ;
        ////    int panelID = count;
        ////    OccurrenceList oOccs = occList;
        ////    string topFileFullName;
        ////    string targetPath = targetDirectory;
        ////    targetAssemblyPath = targetAssembly;
        ////    targetDrawingPath = targetDrawing;
        ////    string panelIDString = System.Convert.ToString(panelID);
        ////    UniqueModule uniquePanels = uniqueModulesCollection;
        ////    //Instead of using "panelID" to create unique folders for all instances, redirect to the GeometryMapIndex
        ////    string geoMapString = System.Convert.ToString(GeometryMapIndex);
        ////    string folderName;
        ////    if (CreateAllCopies==false)
        ////    {
        ////        //if(panelID < 10){
        ////        if(GeometryMapIndex < 10)
        ////        {
        ////            //folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 00" + panelIDString;
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 00" + geoMapString;
        ////        }
        ////        //else if(10 <= panelID && panelID < 100){
        ////        else if(10 <= GeometryMapIndex && GeometryMapIndex < 100)
        ////        {
        ////            //folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 0" + panelIDString;
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 0" + geoMapString;
        ////        }
        ////        else
        ////        {
        ////            //folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " " + panelIDString;
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " " + geoMapString;
        ////        }
        ////    }

        ////    else
        ////    {
        ////        if(panelID < 10)
        ////        {
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 00" + panelIDString;
        ////        }
        ////        else if(10 <= panelID && panelID < 100)
        ////        {
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " 0" + panelIDString;
        ////        }
        ////        else
        ////        {
        ////            folderName = System.IO.Path.GetFileNameWithoutExtension(targetAssemblyPath) + " " + panelIDString;
        ////        }
        ////    }
        ////    //if(panelID < 10){
        ////    //Need to get number of the parent occ, top level name as foldername
        ////    string pathString = System.IO.Path.Combine(targetPath,folderName);

        ////    topFileFullName = oOccs.TargetAssembly.FullDocumentName;
        ////    string topFileNameOnly = System.IO.Path.GetFileName(topFileFullName);
        ////    string topAssemblyNewLocation = System.IO.Path.Combine(pathString,topFileNameOnly);
        ////    //changed this from PanelFilePath, had duplicate properties.


        ////    this.ModulePath = topAssemblyNewLocation;

        ////    TupleList<string,string> filePathPair = new TupleList<string, string>();

        ////    for (int i = 0; i < occList.Items.Count; i++)
        ////    {
        ////        string targetOccPath = occList.Items[i].ReferencedFileDescriptor.FullFileName;
        ////        string newCopyName = System.IO.Path.GetFileName(targetOccPath);
        ////        string newFullCopyName = System.IO.Path.Combine(pathString,newCopyName);
        ////        filePathPair.Add(targetOccPath,newFullCopyName);
        ////    }

        ////    //Check if an earlier panel already made the folder, if not, create it.
        ////    if (System.IO.Directory.Exists(pathString) == false)
        ////    {
        ////        firstTime = true;
        ////        System.IO.Directory.CreateDirectory(pathString);
        ////        AssemblyReplaceRef(oAppServ,oOccs.TargetAssembly,filePathPair,pathString);
        ////        ApprenticeServerDocument oAssDoc;
        ////        oAssDoc = oAppServ.Open(targetAssemblyPath);
        ////        FileSaveAs fileSaver;
        ////        fileSaver = oAppServ.FileSaveAs;
        ////        fileSaver.AddFileToSave(oAssDoc,topAssemblyNewLocation);
        ////        fileSaver.ExecuteSaveCopyAs();

        ////        //Need to copy presentation files if there any.  For now this is only going to work with the top assembly.
        ////        string templateDirectory = System.IO.Path.GetDirectoryName(targetAssemblyPath);
        ////        string[] presFiles = System.IO.Directory.GetFiles(templateDirectory, "*.ipn");
        ////        //If we want the ability to have subassemblies with .ipn files or multiple ones, this will have to be changed
        ////        //to iterate over all the .ipn files.
        ////        if (presFiles.Length !=0)
        ////        {
        ////            string newCopyPresName = System.IO.Path.GetFileName(presFiles[0]);
        ////            string newFullCopyPresName = System.IO.Path.Combine(pathString, newCopyPresName);

        ////            ApprenticeServerDocument presentationDocument = oAppServ.Open(presFiles[0]);
        ////            DocumentDescriptorsEnumerator presFileDescriptors = presentationDocument.ReferencedDocumentDescriptors;
        ////            foreach(DocumentDescriptor refPresDocDescriptor in presFileDescriptors)
        ////            {
        ////                if(refPresDocDescriptor.FullDocumentName == targetAssemblyPath)
        ////                {
        ////                    refPresDocDescriptor.ReferencedFileDescriptor.ReplaceReference(topAssemblyNewLocation);
        ////                    FileSaveAs fileSavePres;
        ////                    fileSavePres = oAppServ.FileSaveAs;
        ////                    fileSavePres.AddFileToSave(presentationDocument,newFullCopyPresName);
        ////                }
        ////            }  
        ////        }
                

        ////        string newCopyDrawingName = System.IO.Path.GetFileName(targetDrawingPath);
        ////        string newFullCopyDrawingName = System.IO.Path.Combine(pathString, newCopyDrawingName);

        ////        if (targetDrawingPath !="")
        ////        {
        ////            ApprenticeServerDocument drawingDoc = oAppServ.Open(targetDrawingPath);
        ////            DocumentDescriptorsEnumerator drawingFileDescriptors = drawingDoc.ReferencedDocumentDescriptors;
        ////            //This needs to be fixed.  It was written with the assumption that only the template assembly would be in 
        ////            //the details and be first in the collection of document descriptors.  Need to iterate through 
        ////            //drawingFileDescriptors and match names and replace correct references.
        ////            //Possibly can use the "filePathPair" object for name matching/reference replacing.
        ////            //drawingFileDescriptors[1].ReferencedFileDescriptor.ReplaceReference(topAssemblyNewLocation);
        ////            foreach(DocumentDescriptor refDocDescriptor in drawingFileDescriptors)
        ////            {
        ////                foreach(Tuple<string,string> pathPair in filePathPair)
        ////                {
        ////                    string newFileNameLower = System.IO.Path.GetFileName(pathPair.Item2);
        ////                    string drawingReferenceLower = System.IO.Path.GetFileName(refDocDescriptor.FullDocumentName);
        ////                    string topAssemblyLower = System.IO.Path.GetFileName(topAssemblyNewLocation);
        ////                    if(topAssemblyLower == drawingReferenceLower)
        ////                    {
        ////                        refDocDescriptor.ReferencedFileDescriptor.ReplaceReference(topAssemblyNewLocation);
        ////                    }
        ////                    if(newFileNameLower == drawingReferenceLower)
        ////                    {
        ////                        refDocDescriptor.ReferencedFileDescriptor.ReplaceReference(pathPair.Item2);
        ////                    }
        ////                }
        ////            }
        ////            //for (int y = 0; y < oDrawingOccs.Count; y++)
        ////            //{
        ////               //string drawingOccName = oDrawingOccs[y + 1].Name;
        ////                //System.Windows.Forms.MessageBox.Show(drawingOccName);
        ////            //}
        ////            FileSaveAs fileSaveDrawing;
        ////            fileSaveDrawing = oAppServ.FileSaveAs;
        ////            fileSaveDrawing.AddFileToSave(drawingDoc, newFullCopyDrawingName);
        ////            fileSaveDrawing.ExecuteSaveCopyAs();
        ////            firstTime = true;
        ////            //Not sure what this is anymore.
        ////            if (uniqueModulesCollection.DetailDocumentPaths.Contains(newFullCopyDrawingName)==false)
        ////            {
        ////                uniqueModulesCollection.DetailDocumentPaths.Add(newFullCopyDrawingName);
        ////            }
        ////        }
        ////    }
        ////    //We only want to call "AssemblyReplaceReference" if a new assembly hasn't been created yet.
        ////}

        //public void PlacePanel(AssemblyDocument assDoc, int constraintVal, string layoutName)
        //{
        //    AssemblyDocument oAssDoc = assDoc;
        //    int constraint = constraintVal;
        //    ComponentOccurrence topOcc;
        //    string layoutDisplayName = layoutName;
        //    topOcc = oCompDef.Occurrences.Add(ModulePath, oMatrix);
        //    ComponentOccurrencesEnumerator topOccSubs = topOcc.SubOccurrences;
        //    int topOccSubsCount = topOccSubs.Count;
        //    ComponentOccurrence layoutOcc = null;
        //    ComponentOccurrence frameOcc = null;
        //    layoutCompDef = null;
        //    for (int i = 0; i < topOccSubsCount; i++)
        //    {
        //        ComponentOccurrence currentSub = topOccSubs[i + 1];
        //        if (currentSub.Name == layoutDisplayName)
        //        {
        //            layoutOcc = currentSub;
        //            layoutCompDef = (PartComponentDefinition)layoutOcc.Definition;

        //        }
        //        if (currentSub.Name == "Frame0001:1")
        //        {
        //            frameOcc = currentSub;
        //            frameCompDef = (AssemblyComponentDefinition)frameOcc.Definition;

        //        }
        //        else
        //        {
        //            topOccSubsList.Add(currentSub);
        //        }
        //    }
        //    int workPointCount = layoutCompDef.WorkPoints.Count;
        //    List<WorkPointProxy> targetProxyList = new List<WorkPointProxy>();
        //    List<WorkPoint> targetWorkPointList = new List<WorkPoint>();
        //    for (int j = 0; j < workPointCount; j++)

        //     //TODO  Need to put logic in that test a layout file for derivedparametertables collection.Count != 0
        //     //then do the copy of the layout file, get the layout file and swap out the document descriptor IN APPRENTICE.
        //    {
        //        WorkPoint currentWP;
        //        currentWP = layoutCompDef.WorkPoints[j+1];
        //        targetWorkPointList.Add(currentWP);
        //        object currentWPProxyObject;
        //        WorkPointProxy currentWPProxy;
        //        layoutOcc.CreateGeometryProxy(currentWP, out currentWPProxyObject);
        //        currentWPProxy = (WorkPointProxy)currentWPProxyObject;
        //        targetProxyList.Add(currentWPProxy);
        //    }
        //    //TODO Fix this to be more intellegent.  What if assembly had two planes (rooms etc.).
        //    WorkPlane targetWorkPlane;
        //    targetWorkPlane = (WorkPlane)layoutCompDef.WorkPlanes[4];
        //    ModuleWorkPointsTarget = targetWorkPointList;
        //    ModuleWorkPointsProxyTarget = targetProxyList;
        //    object wPlaneProxyObject;
        //    WorkPlaneProxy wPlaneProxy;
        //    layoutOcc.CreateGeometryProxy(targetWorkPlane, out wPlaneProxyObject);
        //    wPlaneProxy = (WorkPlaneProxy)wPlaneProxyObject;
        //    ModuleWorkPlaneProxyTarget = wPlaneProxy;

        //    //Workplane constraints needed or not?
        //    //oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(PanelWorkPlaneProxyTarget, layoutCompDef.WorkPlanes[1], 0);

        //    if (firstTime==true)
        //    {
        //        for (int f = 0; f < constraint; f++)
        //        {
        //            //TODO this is uncertain.  It changes from test to test, need to get better handle on the indexing of points.
        //            //oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(PanelWorkPointsProxyTarget[f+1], PanelWorkPointsProxyAssembly[f],0);
        //            oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(ModuleWorkPointsProxyTarget[f+1], ModuleWorkPointsProxyAssembly[f], 0);
        //        }
        //        topOcc.Adaptive = true;
        //        oAssDoc.Update2();
        //        topOcc.Adaptive = false;
        //        targetWorkPlane.Visible = false;
        //        layoutOcc.Visible = false;
        //    }

        //    else
        //    {
        //        //oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(panelWorkPlaneProxyTarget, PanelWorkPlaneProxyAssembly, 0);
        //        //for (int f = 0; f < constraint-2; f++)
        //        for (int f = 0; f < constraint; f++)
        //        {
        //            //TODO this is uncertain.  It changes from test to test, need to get better handle on the indexing of points.
        //            //oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(PanelWorkPointsProxyTarget[f+1], PanelWorkPointsProxyAssembly[f],0);
        //            MateConstraint oMate = oAssDoc.ComponentDefinition.Constraints.AddMateConstraint(ModuleWorkPointsProxyTarget[f+1], ModuleWorkPointsProxyAssembly[f], 0);
        //            if (f>0)
        //            {
        //                //These mate constraints will fail out in space because of double accuracy issues unless they are relaxed some.
        //                oMate.ConstraintLimits.MaximumEnabled = true;
        //                oMate.ConstraintLimits.Maximum.Expression = ".5 in";
        //            }
                    
        //        }
        //        targetWorkPlane.Visible = false;
        //        layoutOcc.Visible = false;
        //    }
        //}

        //public void UpdateParameters()
        //{
        //    //TODO:  This whole method is terrible.  This must be refactored and split up.  The call
        //    //site should also change to be somewhere other than the main program control.
        //    //Gets a list of the Layout parameters from the database
        //    //TupleList<string, double> oldParams = OriginalParameters;
        //    Parameters layoutParams = LayoutCompDef.Parameters;
        //    scopePairs.Add("layoutParams", layoutParams);
        //    string testParam1 = "Module_Width";
        //    string testParam2 = "Module_Height";
        //    List<string> testParams = new List<string>() { testParam1, testParam2 };
        //    double testParamValue = 0;
        //    //if (FrameCompDef != null)
        //    //{
        //        //Parameters frameParams = FrameCompDef.Parameters;
        //        //scopePairs.Add("frameParams", frameParams);
        //    for (int b = 0; b < layoutParams.Count; b++)
        //    {
        //        foreach (string testParam in testParams)
        //        {
        //            if (layoutParams[b + 1].Name == testParam)
        //            {
        //                //changed this from b to b+1?  
        //                testParamValue = System.Convert.ToDouble(layoutParams[b + 1].Value);
        //                //TODO make unit conversion more intelligent, can't always assume standard inches template.
        //                double inchTestValue = testParamValue / 2.54;
        //                if (FrameCompDef != null)
        //                {
        //                    Parameters frameParams = FrameCompDef.Parameters;
        //                    scopePairs.Add("frameParams", frameParams);
        //                    for (int m = 0; m < frameParams.Count; m++)
        //                    {
        //                        string currentExpression = frameParams[m + 1].Expression;
        //                        string[] expressionWords = currentExpression.Split(new Char[] { ' ' });
        //                        for (int e = 0; e < expressionWords.Length; e++)
        //                        {
        //                            if (expressionWords[e] == testParam)
        //                            {
        //                                frameParams[m + 1].Expression = Regex.Replace(currentExpression, testParam, inchTestValue.ToString() + " in");
        //                            }
        //                        }
        //                    }
        //                }

        //                if (TopOccurrences != null)
        //                {
        //                    foreach (ComponentOccurrence topOcc in TopOccurrences)
        //                    {
        //                        if (topOcc.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
        //                        {
        //                            PartComponentDefinition topOccPartDef = (PartComponentDefinition)topOcc.Definition;
        //                            Parameters topOccPartParams = topOccPartDef.Parameters;
        //                            scopePairs.Add("topOccPartParams", topOccPartParams);
        //                            for (int u = 0; u < topOccPartParams.Count; u++)
        //                            {
        //                                string currentExpression = topOccPartParams[u + 1].Expression;
        //                                string[] expressionWords = currentExpression.Split(new Char[] { ' ' });
        //                                for (int p = 0; p < expressionWords.Length; p++)
        //                                {
        //                                    if (expressionWords[p] == testParam)
        //                                    {
        //                                        topOccPartParams[u + 1].Expression = Regex.Replace(currentExpression, testParam, inchTestValue.ToString() + " in");
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public void AssemblyReplaceRef(ApprenticeServer appServ, ApprenticeServerDocument assDoc,TupleList<string,string> namePair,string folderPath)
        //{
        //    ApprenticeServer oAppServ = appServ;
        //    ApprenticeServerDocument oAssDoc = assDoc;
        //    OccurrenceList newOccs = new OccurrenceList(oAppServ,oAssDoc);
        //    string pathString = folderPath;
        //    List<string> patternComponentsList = new List<string>();
        //    for (int i = 0; i < newOccs.Items.Count; i++) {
        //        if(newOccs.Items[i].DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject){
        //            //if (newOccs.Items[i].ParentOccurrence == null) {
        //                for (int f = 0; f < namePair.Count; f++) {
        //                    if(namePair[f].Item1 == newOccs.Items[i].ReferencedFileDescriptor.FullFileName){
        //                        if(patternComponentsList.Contains(namePair[f].Item1)){
        //                            newOccs.Items[i].ReferencedDocumentDescriptor.ReferencedFileDescriptor.ReplaceReference(namePair[f].Item2);
        //                        }
        //                        else{
        //                            if (!System.IO.File.Exists(namePair[f].Item2))
        //                            {
        //                                oAppServ.FileManager.CopyFile(namePair[f].Item1,namePair[f].Item2);
        //                                newOccs.Items[i].ReferencedDocumentDescriptor.ReferencedFileDescriptor.ReplaceReference(namePair[f].Item2);
        //                            }
								    
        //                            patternComponentsList.Add(namePair[f].Item1);
        //                        }
        //                    }
        //                }
        //        }
        //        else if(newOccs.Items[i].DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject){
        //            for (int n = 0; n < namePair.Count; n++) {
        //                if(namePair[n].Item1 == newOccs.Items[i].ReferencedFileDescriptor.FullFileName){
        //                    ApprenticeServerDocument oSubAssDoc = oAppServ.Open(newOccs.Items[i].ReferencedFileDescriptor.FullFileName);
        //                    AssemblyReplaceRef(oAppServ,oSubAssDoc,namePair,pathString);
        //                    FileSaveAs fileSave;
        //                    fileSave = oAppServ.FileSaveAs;
        //                    string newFilePath = namePair[n].Item2;
        //                    fileSave.AddFileToSave(oSubAssDoc,newFilePath);

        //                    //TODO Need to code a check at the beginning (copyutilmain.xaml.cs) that determines if the target directory is in the Project or not. 
        //                    //Apprentice fails otherwise.
        //                    //TODO Also, Inventor needs to be running for this not to fail.
        //                    fileSave.ExecuteSaveCopyAs();
        //                    newOccs.Items[i].ReferencedDocumentDescriptor.ReferencedFileDescriptor.ReplaceReference(namePair[n].Item2);
        //                }
        //            }
        //        }
        //    }
        //}

        //TODO Refactor "Panel" to "Module"
        //TODO Add "UpdateLocation" method (if using db locations from Revit)

	}
}
