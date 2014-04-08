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
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DSInventorNodes.TestingHelpers
{
    [RegisterForTrace]

    public class Bools
    {

        #region Internal properties
        #endregion

        #region Private constructors
        private Bools()
        {
        }
        #endregion

        #region Private mutators
        #endregion

        #region Public properties
        public bool GetTrue
        {
            get { return true; }
        }

        public bool GetFalse
        {
            get { return false; }
        }
        #endregion

        #region Public static constructors

        public static Bools GetBools()
        {
            return new Bools();
        }

        #endregion

        #region Internal static constructors
        #endregion 
    }
}
