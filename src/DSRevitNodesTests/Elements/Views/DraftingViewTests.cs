﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSRevitNodes;
using DSRevitNodes.Elements;
using DSRevitNodes.GeometryObjects;
using NUnit.Framework;
using RevitServices.Persistence;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DSRevitNodesTests
{
    [TestFixture]
    class DraftingViewTests
    {
        [Test]
        public void ByName_ValidArgs()
        {
            var view = DSDraftingView.ByName("poodle");
            Assert.NotNull(view);
            Assert.IsTrue(DocumentManager.GetInstance().ElementExistsInDocument(view.InternalElement.Id));
        }

        [Test]
        public void ByName_NullArgs()
        {
            Assert.Throws(typeof(ArgumentNullException), () => DSSectionView.ByBoundingBox(null));
        }
    }
}