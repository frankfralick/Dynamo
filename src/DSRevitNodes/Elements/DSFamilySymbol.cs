﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.DB;
using DSNodeServices;
using DSRevitNodes.Elements;
using RevitServices.Persistence;
using RevitServices.Transactions;

namespace DSRevitNodes.Elements
{
    /// <summary>
    /// A Revit FamilySymbol
    /// </summary>
    [RegisterForTrace]
    public class DSFamilySymbol: AbstractElement
    {

        #region Internal Properties

        /// <summary>
        /// Internal wrapper property
        /// </summary>
        internal Autodesk.Revit.DB.FamilySymbol InternalFamilySymbol
        {
            get;
            private set;
        }

        /// <summary>
        /// Reference to the Element
        /// </summary>
        public override Autodesk.Revit.DB.Element InternalElement
        {
            get { return InternalFamilySymbol; }
        }

        #endregion

        #region Private constructors

        /// <summary>
        /// Private constructor for build a DSFamilySymbol
        /// </summary>
        /// <param name="symbol"></param>
        private DSFamilySymbol(Autodesk.Revit.DB.FamilySymbol symbol)
        {
            InternalSetFamilySymbol(symbol);
        }

        #endregion

        #region Private mutators

        /// <summary>
        /// Set the internal model of the family symbol along with its ElementId and UniqueId
        /// </summary>
        /// <param name="symbol"></param>
        private void InternalSetFamilySymbol(Autodesk.Revit.DB.FamilySymbol symbol)
        {
            this.InternalFamilySymbol = symbol;
            this.InternalElementId = symbol.Id;
            this.InternalUniqueId = symbol.UniqueId;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get the name of this Family Symbol
        /// </summary>
        public string Name
        {
            get
            {
                return InternalFamilySymbol.Name;
            }
        }

        /// <summary>
        /// Get the parent family of this FamilySymbol
        /// </summary>
        public DSFamily Family
        {
            get
            {
                return DSFamily.FromExisting(this.InternalFamilySymbol.Family, true);
            }
        }

        #endregion

        #region Public static constructors

        /// <summary>
        /// Select a FamilySymbol given it's parent Family and the FamilySymbol's name.
        /// </summary>
        /// <param name="family">The FamilySymbol's parent Family</param>
        /// <param name="name">The name of the FamilySymbol</param>
        /// <returns></returns>
        public static DSFamilySymbol ByFamilyAndName(DSFamily family, string name)
        {
            if (family == null)
            {
                throw new ArgumentNullException("family");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            // obtain the family symbol with the provided name
            var symbol =
                family.InternalFamily.Symbols.Cast<Autodesk.Revit.DB.FamilySymbol>().FirstOrDefault(x => x.Name == name);

            if (symbol == null)
            {
               throw new Exception(String.Format("A FamilySymbol with the specified name, {0}, does not exist in the Family", name));
            }

            return new DSFamilySymbol(symbol)
            {
                IsRevitOwned = true
            };
        }

        /// <summary>
        /// Select a FamilySymbol given it's name.  This method will return the first FamilySymbol it finds if there are
        /// two or more FamilySymbol's with the same name.
        /// </summary>
        /// <param name="name">The name of the FamilySymbol</param>
        /// <returns></returns>
        public static DSFamilySymbol ByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            // look up the loaded family
            var symbol = DocumentManager.GetInstance()
                .ElementsOfType<Autodesk.Revit.DB.FamilySymbol>()
                .FirstOrDefault(x => x.Name == name);

            if (symbol == null)
            {
                throw new Exception(String.Format("A FamilySymbol with the specified name, {0}, does not exist in the document", name));
            }

            return new DSFamilySymbol(symbol)
            {
                IsRevitOwned = true
            };
        }

        #endregion

        #region Internal static constructors

        /// <summary>
        /// Obtain a FamilySymbol by selection. 
        /// </summary>
        /// <param name="familySymbol"></param>
        /// <param name="isRevitOwned"></param>
        /// <returns></returns>
        internal static DSFamilySymbol FromExisting(Autodesk.Revit.DB.FamilySymbol familySymbol, bool isRevitOwned)
        {
            return new DSFamilySymbol(familySymbol)
            {
                IsRevitOwned = isRevitOwned
            };
        }

        #endregion

        #region ToString override

        public override string ToString()
        {
            return String.Format("FamilySymbol: {0}, Family: {1}", InternalFamilySymbol.Name,
                InternalFamilySymbol.Family.Name);
        }

        #endregion



    }
}
