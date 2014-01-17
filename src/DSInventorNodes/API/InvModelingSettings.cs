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

namespace DSInventorNodes
{
    [RegisterForTrace]
    public class InvModelingSettings
    {
        #region Internal properties
        internal Inventor.ModelingSettings InternalModelingSettings { get; set; }

        internal Object InternalApplication
        {
            get { return ModelingSettingsInstance.Application; }
        }

        internal Inv_Document InternalParent
        {
            get { return Inv_Document.ByInv_Document(ModelingSettingsInstance.Parent); }
        }


        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(ModelingSettingsInstance.Type); }
        }


        internal InvUserCoordinateSystemSettings InternalUserCoordinateSystemSettings
        {
            get { return InvUserCoordinateSystemSettings.ByInvUserCoordinateSystemSettings(ModelingSettingsInstance.UserCoordinateSystemSettings); }
        }


        internal bool InternalAdaptivelyUsedInAssembly { get; set; }

        internal bool InternalAdvancedFeatureValidation { get; set; }

        internal bool InternalAllowSectioningThruPart { get; set; }

        internal bool InternalCompactModelHistory { get; set; }

        internal double InternalInitialDrawingViewHeight { get; set; }

        internal double InternalInitialDrawingViewWidth { get; set; }

        internal InteractiveContactAnalysisEnum InternalInteractiveContactAnalysis { get; set; }

        internal InteractiveContactSurfacesEnum InternalInteractiveContactSurfaces { get; set; }

        internal bool InternalMaintainEnhancedGraphicsDetail { get; set; }

        internal string InternalSnap3DAngle { get; set; }

        internal string InternalSnap3DDistance { get; set; }

        internal string InternalSolidBodyNamePrefix { get; set; }

        internal string InternalSurfaceBodyNamePrefix { get; set; }

        internal ModelDiameterFromThreadEnum InternalTappedHoleDiameter { get; set; }

        internal bool InternalUpdateLegacyHoles { get; set; }
        #endregion

        #region Private constructors
        private InvModelingSettings(InvModelingSettings invModelingSettings)
        {
            InternalModelingSettings = invModelingSettings.InternalModelingSettings;
        }

        private InvModelingSettings(Inventor.ModelingSettings invModelingSettings)
        {
            InternalModelingSettings = invModelingSettings;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.ModelingSettings ModelingSettingsInstance
        {
            get { return InternalModelingSettings; }
            set { InternalModelingSettings = value; }
        }

        public Object Application
        {
            get { return InternalApplication; }
        }

        public Inv_Document Parent
        {
            get { return InternalParent; }
        }

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        public InvUserCoordinateSystemSettings UserCoordinateSystemSettings
        {
            get { return InternalUserCoordinateSystemSettings; }
        }

        public Invbool AdaptivelyUsedInAssembly
        {
            get { return InternalAdaptivelyUsedInAssembly; }
            set { InternalAdaptivelyUsedInAssembly = value; }
        }

        public Invbool AdvancedFeatureValidation
        {
            get { return InternalAdvancedFeatureValidation; }
            set { InternalAdvancedFeatureValidation = value; }
        }

        public Invbool AllowSectioningThruPart
        {
            get { return InternalAllowSectioningThruPart; }
            set { InternalAllowSectioningThruPart = value; }
        }

        public Invbool CompactModelHistory
        {
            get { return InternalCompactModelHistory; }
            set { InternalCompactModelHistory = value; }
        }

        public Invdouble InitialDrawingViewHeight
        {
            get { return InternalInitialDrawingViewHeight; }
            set { InternalInitialDrawingViewHeight = value; }
        }

        public Invdouble InitialDrawingViewWidth
        {
            get { return InternalInitialDrawingViewWidth; }
            set { InternalInitialDrawingViewWidth = value; }
        }

        public InvInteractiveContactAnalysisEnum InteractiveContactAnalysis
        {
            get { return InternalInteractiveContactAnalysis; }
            set { InternalInteractiveContactAnalysis = value; }
        }

        public InvInteractiveContactSurfacesEnum InteractiveContactSurfaces
        {
            get { return InternalInteractiveContactSurfaces; }
            set { InternalInteractiveContactSurfaces = value; }
        }

        public Invbool MaintainEnhancedGraphicsDetail
        {
            get { return InternalMaintainEnhancedGraphicsDetail; }
            set { InternalMaintainEnhancedGraphicsDetail = value; }
        }

        public Invstring Snap3DAngle
        {
            get { return InternalSnap3DAngle; }
            set { InternalSnap3DAngle = value; }
        }

        public Invstring Snap3DDistance
        {
            get { return InternalSnap3DDistance; }
            set { InternalSnap3DDistance = value; }
        }

        public Invstring SolidBodyNamePrefix
        {
            get { return InternalSolidBodyNamePrefix; }
            set { InternalSolidBodyNamePrefix = value; }
        }

        public Invstring SurfaceBodyNamePrefix
        {
            get { return InternalSurfaceBodyNamePrefix; }
            set { InternalSurfaceBodyNamePrefix = value; }
        }

        public InvModelDiameterFromThreadEnum TappedHoleDiameter
        {
            get { return InternalTappedHoleDiameter; }
            set { InternalTappedHoleDiameter = value; }
        }

        public Invbool UpdateLegacyHoles
        {
            get { return InternalUpdateLegacyHoles; }
            set { InternalUpdateLegacyHoles = value; }
        }

        #endregion
        #region Public static constructors
        public static InvModelingSettings ByInvModelingSettings(InvModelingSettings invModelingSettings)
        {
            return new InvModelingSettings(invModelingSettings);
        }
        public static InvModelingSettings ByInvModelingSettings(Inventor.ModelingSettings invModelingSettings)
        {
            return new InvModelingSettings(invModelingSettings);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
