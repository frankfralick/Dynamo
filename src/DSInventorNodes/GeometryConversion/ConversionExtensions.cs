﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace DSInventorNodes.GeometryConversion
{
    [Browsable(false)]
    public static class ConversionExtensions
    {
        #region Proto -> Inventor types
        #endregion

        #region Inventor -> Proto types
        public static Autodesk.DesignScript.Geometry.Point ToPoint(this Inventor.Point xyz)
        {
            return Autodesk.DesignScript.Geometry.Point.ByCoordinates(xyz.X, xyz.Y, xyz.Z);
        }
        #endregion
    }
}
