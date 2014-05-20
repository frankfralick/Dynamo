using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace InventorLibrary.ModulePlacement
{
    public interface IPointsList : INotifyPropertyChanged
    {
        List<List<Point>> PointsList { get; set; }
        List<List<Point>> OldPointsList { get; set; }
        bool IsDirty { get; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
