using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

using InventorServices.Utilities;


namespace DSInventorNodes.ModulePlacement
{
    public class UniqueModule : IDisposable
    {
        List<Module> module;
        int constraintCount;
        List<double> distances;
        TupleList<int, List<double>> allDistancesList = new TupleList<int, List<double>>();
        TupleList<int, List<double>> uniqueDistancesList = new TupleList<int, List<double>>();
        TupleList<int, int> instanceGeometryMap = new TupleList<int, int>();

        double tolerance = .5;

        static string logFilePath = "C:\\Projects\\InventorCopyCreator\\uniquePanelsLog12.txt";
        System.IO.StreamWriter logFile = new System.IO.StreamWriter(logFilePath);

        TupleList<int, string> detailDocuments = new TupleList<int, string>();

        public List<string> detailDocPaths = new List<string>();

        public List<string> DetailDocumentPaths
        {
            get { return detailDocPaths; }
            set { detailDocPaths = value; }
        }

        public TupleList<int, List<double>> AllPanelDistances
        {
            get { return allDistancesList; }
            set { allDistancesList = value; }
        }

        public TupleList<int, List<double>> UniquePanelDistances
        {
            get { return uniqueDistancesList; }
            set { uniqueDistancesList = value; }
        }

        public TupleList<int, int> InstanceGeometryMap
        {
            get { return instanceGeometryMap; }
            set { instanceGeometryMap = value; }
        }

        public double ConstraintTolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public int ConstraintCount
        {
            get { return constraintCount; }
            set { constraintCount = value; }
        }

        public UniqueModule(List<Module> panelList)
        {
            module = panelList;
            //for now we will assume that all the panels contain the same number of constraints
            constraintCount = module[0].ModulePoints.Count;
            GenerateDistances();
        }

        public void IdentifyDuplicates()
        {
            //If the constraintCount is 3 or less, we can just check that the distance from point1-point2,
            //point2-point3, and point3-point1 are the same.  
            //If the constraintCount is greater than 3, we must check constraintCount-2 additional distances.
            //There is a more efficient way to do this but this is simple.
            List<List<double>> tempDistList = new List<List<double>>();
            for (int i = 0; i < AllPanelDistances.Count; i++)
            {
                //The unique geometries list is empty, put the first one in it.
                if (i==0)
                {
                    UniquePanelDistances.Add(AllPanelDistances[i]);
                    InstanceGeometryMap.Add(i, i);
                    StringBuilder firstLine = new StringBuilder();
                    firstLine.Append(i.ToString() + ", " + i.ToString());
                    logFile.WriteLine(firstLine.ToString());
                }
                else
                {
                    //This delegate is for comparing to the specified constraint tolerance.
                    Func<double, double, double> currentTolerance = (double item1, double item2) => System.Math.Abs(item1 - item2);
                    int uniqueOuterCounter = 0;
                    for (int k = 0; k < UniquePanelDistances.Count; k++)
                    {
                        int uniqueInnerCounter = 0;
                        for (int r = 0; r < AllPanelDistances[i].Item2.Count; r++)
                        {
                            double thisCurrentTolerance = currentTolerance(UniquePanelDistances[k].Item2[r], AllPanelDistances[i].Item2[r]);
                            //StringBuilder dataLine = new StringBuilder();
                            //dataLine.Append("All Panel Distances[" + i.ToString() + "] " + "Item2[" + r.ToString() + "]    "
                                //+ AllPanelDistances[i].Item2[r] .ToString()+ "    Unique Panel Distances[" + k.ToString() + "] " + "Item2[" + r.ToString() 
                               // + "]    "+UniquePanelDistances[k].Item2[r].ToString()
                               // +"    Tolerance:  " 
                               // + thisCurrentTolerance.ToString());
                            //logFile.WriteLine(dataLine.ToString());
                            if (thisCurrentTolerance>ConstraintTolerance)
                            {
                                uniqueInnerCounter++;
                            }
                            
                        }
                        //After looping through all the constraint sets, if there were any out of tolerance, then this geometry could be 
                        //unique.  This counter has tick up for all geometries in the unique set for it to be added to the set.
                        if (uniqueInnerCounter != 0)
                        {
                            uniqueOuterCounter++;
                        }

                        if (uniqueInnerCounter==0)
                        {
                            InstanceGeometryMap.Add(i, k);
                            StringBuilder otherLine = new StringBuilder();
                            otherLine.Append(i.ToString() + ", " + k.ToString());
                            logFile.WriteLine(otherLine.ToString());
                        }
                        
                    }
                    //If this evaluates to true, then the current geometry did not match any of the geometry in the 
                    //unique list.
                    if (uniqueOuterCounter==UniquePanelDistances.Count)
                    {
                        UniquePanelDistances.Add(AllPanelDistances[i]);
                        //StringBuilder addedLine = new StringBuilder();
                        //addedLine.Append("FOUND A UNIQUE PANEL:     AllPanelDistances["+i.ToString()+"]");
                        //logFile.WriteLine(addedLine.ToString());
                        InstanceGeometryMap.Add(i, UniquePanelDistances.Count-1);
                        StringBuilder otherOtherLine = new StringBuilder();
                        otherOtherLine.Append(i.ToString() + ", " + (UniquePanelDistances.Count-1).ToString());
                        logFile.WriteLine(otherOtherLine.ToString());

                    }
                }
            }
            logFile.Close();
            for (int t = 0; t < module.Count; t++)
            {
                //This sets the property on the panel class instance that identifies
                //which copied assembly should be used for its instance. 
                module[t].GeometryMapIndex = InstanceGeometryMap[t].Item2;
            }
        }

        private void GenerateDistances()
        {
            for (int i = 0; i < module.Count; i++)
            {
                List<double> tempDistances = new List<double>();
                //We are going to get the distance of Pn-Pn+1 and Plast-Pfirst. If addlCheck > 2,
                //we need to check Pn-Pn+2 for n < addlCheck+1.
                int addlCheck = 0;
                if (constraintCount>3)
                {
                    addlCheck = module[i].ModulePoints.Count - 2;
                }
                for (int j = 0; j < constraintCount-1; j++)
                {
                    double tempDist = DistanceToPoint(module[i].ModulePoints[j], module[i].ModulePoints[j + 1]);
                    tempDistances.Add(tempDist);
                }
                double firstToLastDist = DistanceToPoint(module[i].ModulePoints[constraintCount - 1], module[i].ModulePoints[0]);
                tempDistances.Add(firstToLastDist);
                //Add the additional check distances.
                for (int y = 0; y < addlCheck; y++)
                {
                    double tempAddlCheck = DistanceToPoint(module[i].ModulePoints[y], module[i].ModulePoints[y + 2]);
                    tempDistances.Add(tempAddlCheck);
                }

                allDistancesList.Add(i, tempDistances);
            }
        }

        public double DistanceToPoint(Point start, Point end)
        {
            double dx = start.X - end.X;
            double dy = start.Y - end.Y;
            double dz = start.Z - end.Z;
            double distanceSquared = System.Math.Sqrt(System.Math.Pow(dx, 2) + System.Math.Pow(dy, 2) + System.Math.Pow(dz, 2));
            return distanceSquared;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                logFile.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
