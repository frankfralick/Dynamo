﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

using InventorServices;

namespace DSInventorNodes.GeometryConversion
{
    [Browsable(false)]
    public static class ConversionExtensions
    {
        #region Proto -> Inventor types
        public static Inventor.Point ToPoint(this Autodesk.DesignScript.Geometry.Point xyz)
        {
            TransientGeometry transGeo = InventorServices.Persistence.InventorPersistenceManager.InventorApplication.TransientGeometry;
            return transGeo.CreatePoint(xyz.X, xyz.Y, xyz.Z);
        }
        #endregion

        #region Inventor -> Proto types
        public static Autodesk.DesignScript.Geometry.Point ToPoint(this Inventor.Point xyz)
        {
            return Autodesk.DesignScript.Geometry.Point.ByCoordinates(xyz.X, xyz.Y, xyz.Z);
        }
        #endregion
    }
}