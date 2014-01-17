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
    public class InvDisplaySettings
    {
        #region Internal properties
        internal Inventor.DisplaySettings InternalDisplaySettings { get; set; }

        internal Object InternalApplication
        {
            get { return DisplaySettingsInstance.Application; }
        }

        internal InvGroundPlaneSettings InternalGroundPlaneSettings
        {
            get { return InvGroundPlaneSettings.ByInvGroundPlaneSettings(DisplaySettingsInstance.GroundPlaneSettings); }
        }


        internal Object InternalParent
        {
            get { return DisplaySettingsInstance.Parent; }
        }

        internal InvObjectTypeEnum InternalType
        {
            get { return InvObjectTypeEnum.ByInvObjectTypeEnum(DisplaySettingsInstance.Type); }
        }


        internal bool InternalAreTexturesOn { get; set; }

        internal bool InternalDepthDimming { get; set; }

        internal bool InternalDisplaySilhouettes { get; set; }

        internal Color InternalEdgeColor { get; set; }

        internal int InternalHiddenLineDimmingPercent { get; set; }

        internal DisplayModeEnum InternalNewWindowDisplayMode { get; set; }

        internal ProjectionTypeEnum InternalNewWindowProjectionType { get; set; }

        internal bool InternalNewWindowShowAmbientShadows { get; set; }

        internal bool InternalNewWindowShowGroundPlane { get; set; }

        internal bool InternalNewWindowShowGroundReflections { get; set; }

        internal bool InternalNewWindowShowGroundShadows { get; set; }

        internal bool InternalNewWindowShowObjectShadows { get; set; }

        internal RayTracingQualityEnum InternalRayTracingQuality { get; set; }

        internal bool InternalSolidLinesForHiddenEdges { get; set; }

        internal bool InternalUseRayTracingForRealisticDisplay { get; set; }
        #endregion

        #region Private constructors
        private InvDisplaySettings(InvDisplaySettings invDisplaySettings)
        {
            InternalDisplaySettings = invDisplaySettings.InternalDisplaySettings;
        }

        private InvDisplaySettings(Inventor.DisplaySettings invDisplaySettings)
        {
            InternalDisplaySettings = invDisplaySettings;
        }
        #endregion

        #region Private methods
        #endregion

        #region Public properties
        public Inventor.DisplaySettings DisplaySettingsInstance
        {
            get { return InternalDisplaySettings; }
            set { InternalDisplaySettings = value; }
        }

        public Object Application
        {
            get { return InternalApplication; }
        }

        public InvGroundPlaneSettings GroundPlaneSettings
        {
            get { return InternalGroundPlaneSettings; }
        }

        public Object Parent
        {
            get { return InternalParent; }
        }

        public InvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        public Invbool AreTexturesOn
        {
            get { return InternalAreTexturesOn; }
            set { InternalAreTexturesOn = value; }
        }

        public Invbool DepthDimming
        {
            get { return InternalDepthDimming; }
            set { InternalDepthDimming = value; }
        }

        public Invbool DisplaySilhouettes
        {
            get { return InternalDisplaySilhouettes; }
            set { InternalDisplaySilhouettes = value; }
        }

        public InvColor EdgeColor
        {
            get { return InternalEdgeColor; }
            set { InternalEdgeColor = value; }
        }

        public Invint HiddenLineDimmingPercent
        {
            get { return InternalHiddenLineDimmingPercent; }
            set { InternalHiddenLineDimmingPercent = value; }
        }

        public InvDisplayModeEnum NewWindowDisplayMode
        {
            get { return InternalNewWindowDisplayMode; }
            set { InternalNewWindowDisplayMode = value; }
        }

        public InvProjectionTypeEnum NewWindowProjectionType
        {
            get { return InternalNewWindowProjectionType; }
            set { InternalNewWindowProjectionType = value; }
        }

        public Invbool NewWindowShowAmbientShadows
        {
            get { return InternalNewWindowShowAmbientShadows; }
            set { InternalNewWindowShowAmbientShadows = value; }
        }

        public Invbool NewWindowShowGroundPlane
        {
            get { return InternalNewWindowShowGroundPlane; }
            set { InternalNewWindowShowGroundPlane = value; }
        }

        public Invbool NewWindowShowGroundReflections
        {
            get { return InternalNewWindowShowGroundReflections; }
            set { InternalNewWindowShowGroundReflections = value; }
        }

        public Invbool NewWindowShowGroundShadows
        {
            get { return InternalNewWindowShowGroundShadows; }
            set { InternalNewWindowShowGroundShadows = value; }
        }

        public Invbool NewWindowShowObjectShadows
        {
            get { return InternalNewWindowShowObjectShadows; }
            set { InternalNewWindowShowObjectShadows = value; }
        }

        public InvRayTracingQualityEnum RayTracingQuality
        {
            get { return InternalRayTracingQuality; }
            set { InternalRayTracingQuality = value; }
        }

        public Invbool SolidLinesForHiddenEdges
        {
            get { return InternalSolidLinesForHiddenEdges; }
            set { InternalSolidLinesForHiddenEdges = value; }
        }

        public Invbool UseRayTracingForRealisticDisplay
        {
            get { return InternalUseRayTracingForRealisticDisplay; }
            set { InternalUseRayTracingForRealisticDisplay = value; }
        }

        #endregion
        #region Public static constructors
        public static InvDisplaySettings ByInvDisplaySettings(InvDisplaySettings invDisplaySettings)
        {
            return new InvDisplaySettings(invDisplaySettings);
        }
        public static InvDisplaySettings ByInvDisplaySettings(Inventor.DisplaySettings invDisplaySettings)
        {
            return new InvDisplaySettings(invDisplaySettings);
        }
        #endregion

        #region Public methods
        #endregion
    }
}
