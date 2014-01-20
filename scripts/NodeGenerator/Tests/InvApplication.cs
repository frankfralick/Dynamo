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
    public class InvApplication
    {
        #region Internal properties
        internal Inventor.Application InternalApplication { get; set; }

        internal InvInv_AppPerformanceMonitor Internal_AppPerformanceMonitor
        {
            get { return InvInv_AppPerformanceMonitor.ByInvInv_AppPerformanceMonitor(ApplicationInstance._AppPerformanceMonitor); }
        }


        internal bool Internal_CanReplayTranscript
        {
            get { return ApplicationInstance._CanReplayTranscript; }
        }

        internal InvInvDebugInstrumentation Internal_DebugInstrumentation
        {
            get { return InvInvDebugInstrumentation.ByInvInvDebugInstrumentation(ApplicationInstance._DebugInstrumentation); }
        }


        internal string Internal_TranscriptFileName
        {
            get { return ApplicationInstance._TranscriptFileName; }
        }

        internal InvInvColorScheme InternalActiveColorScheme
        {
            get { return InvInvColorScheme.ByInvInvColorScheme(ApplicationInstance.ActiveColorScheme); }
        }


        internal InvInv_Document InternalActiveDocument
        {
            get { return InvInv_Document.ByInvInv_Document(ApplicationInstance.ActiveDocument); }
        }


        internal InvInvDocumentTypeEnum InternalActiveDocumentType
        {
            get { return InvInvDocumentTypeEnum.ByInvInvDocumentTypeEnum(ApplicationInstance.ActiveDocumentType); }
        }


        internal InvInv_Document InternalActiveEditDocument
        {
            get { return InvInv_Document.ByInvInv_Document(ApplicationInstance.ActiveEditDocument); }
        }


        internal InvObject InternalActiveEditObject
        {
            get { return ApplicationInstance.ActiveEditObject; }
        }

        internal InvInvEnvironment InternalActiveEnvironment
        {
            get { return InvInvEnvironment.ByInvInvEnvironment(ApplicationInstance.ActiveEnvironment); }
        }


        internal InvInvView InternalActiveView
        {
            get { return InvInvView.ByInvInvView(ApplicationInstance.ActiveView); }
        }


        internal string InternalAllUsersAppDataPath
        {
            get { return ApplicationInstance.AllUsersAppDataPath; }
        }

        internal InvInvApplicationAddIns InternalApplicationAddIns
        {
            get { return InvInvApplicationAddIns.ByInvInvApplicationAddIns(ApplicationInstance.ApplicationAddIns); }
        }


        internal InvInvApplicationEvents InternalApplicationEvents
        {
            get { return InvInvApplicationEvents.ByInvInvApplicationEvents(ApplicationInstance.ApplicationEvents); }
        }


        internal InvInvAssemblyEvents InternalAssemblyEvents
        {
            get { return InvInvAssemblyEvents.ByInvInvAssemblyEvents(ApplicationInstance.AssemblyEvents); }
        }


        internal InvInvAssemblyOptions InternalAssemblyOptions
        {
            get { return InvInvAssemblyOptions.ByInvInvAssemblyOptions(ApplicationInstance.AssemblyOptions); }
        }


        internal InvInvAssetLibraries InternalAssetLibraries
        {
            get { return InvInvAssetLibraries.ByInvInvAssetLibraries(ApplicationInstance.AssetLibraries); }
        }


        internal InvInvCameraEvents InternalCameraEvents
        {
            get { return InvInvCameraEvents.ByInvInvCameraEvents(ApplicationInstance.CameraEvents); }
        }


        internal InvInvChangeManager InternalChangeManager
        {
            get { return InvInvChangeManager.ByInvInvChangeManager(ApplicationInstance.ChangeManager); }
        }


        internal InvInvColorSchemes InternalColorSchemes
        {
            get { return InvInvColorSchemes.ByInvInvColorSchemes(ApplicationInstance.ColorSchemes); }
        }


        internal InvInvCommandManager InternalCommandManager
        {
            get { return InvInvCommandManager.ByInvInvCommandManager(ApplicationInstance.CommandManager); }
        }


        internal InvInvContentCenter InternalContentCenter
        {
            get { return InvInvContentCenter.ByInvInvContentCenter(ApplicationInstance.ContentCenter); }
        }


        internal InvInvContentCenterOptions InternalContentCenterOptions
        {
            get { return InvInvContentCenterOptions.ByInvInvContentCenterOptions(ApplicationInstance.ContentCenterOptions); }
        }


        internal string InternalCurrentUserAppDataPath
        {
            get { return ApplicationInstance.CurrentUserAppDataPath; }
        }

        internal InvInvDesignProjectManager InternalDesignProjectManager
        {
            get { return InvInvDesignProjectManager.ByInvInvDesignProjectManager(ApplicationInstance.DesignProjectManager); }
        }


        internal InvInvDisplayOptions InternalDisplayOptions
        {
            get { return InvInvDisplayOptions.ByInvInvDisplayOptions(ApplicationInstance.DisplayOptions); }
        }


        internal InvInvDocuments InternalDocuments
        {
            get { return InvInvDocuments.ByInvInvDocuments(ApplicationInstance.Documents); }
        }


        internal InvInvDrawingOptions InternalDrawingOptions
        {
            get { return InvInvDrawingOptions.ByInvInvDrawingOptions(ApplicationInstance.DrawingOptions); }
        }


        internal InvInvEnvironmentBaseCollection InternalEnvironmentBaseCollection
        {
            get { return InvInvEnvironmentBaseCollection.ByInvInvEnvironmentBaseCollection(ApplicationInstance.EnvironmentBaseCollection); }
        }


        internal InvInvEnvironments InternalEnvironments
        {
            get { return InvInvEnvironments.ByInvInvEnvironments(ApplicationInstance.Environments); }
        }


        internal InvInvErrorManager InternalErrorManager
        {
            get { return InvInvErrorManager.ByInvInvErrorManager(ApplicationInstance.ErrorManager); }
        }


        internal InvInvAssetsEnumerator InternalFavoriteAssets
        {
            get { return InvInvAssetsEnumerator.ByInvInvAssetsEnumerator(ApplicationInstance.FavoriteAssets); }
        }


        internal InvInvFileAccessEvents InternalFileAccessEvents
        {
            get { return InvInvFileAccessEvents.ByInvInvFileAccessEvents(ApplicationInstance.FileAccessEvents); }
        }


        internal InvInvFileLocations InternalFileLocations
        {
            get { return InvInvFileLocations.ByInvInvFileLocations(ApplicationInstance.FileLocations); }
        }


        internal InvInvFileManager InternalFileManager
        {
            get { return InvInvFileManager.ByInvInvFileManager(ApplicationInstance.FileManager); }
        }


        internal InvInvFileOptions InternalFileOptions
        {
            get { return InvInvFileOptions.ByInvInvFileOptions(ApplicationInstance.FileOptions); }
        }


        internal InvInvFileUIEvents InternalFileUIEvents
        {
            get { return InvInvFileUIEvents.ByInvInvFileUIEvents(ApplicationInstance.FileUIEvents); }
        }


        internal InvInvGeneralOptions InternalGeneralOptions
        {
            get { return InvInvGeneralOptions.ByInvInvGeneralOptions(ApplicationInstance.GeneralOptions); }
        }


        internal InvInvHardwareOptions InternalHardwareOptions
        {
            get { return InvInvHardwareOptions.ByInvInvHardwareOptions(ApplicationInstance.HardwareOptions); }
        }


        internal InvInvHelpManager InternalHelpManager
        {
            get { return InvInvHelpManager.ByInvInvHelpManager(ApplicationInstance.HelpManager); }
        }


        internal InvInviFeatureOptions InternaliFeatureOptions
        {
            get { return InvInviFeatureOptions.ByInvInviFeatureOptions(ApplicationInstance.iFeatureOptions); }
        }


        internal string InternalInstallPath
        {
            get { return ApplicationInstance.InstallPath; }
        }

        internal bool InternalIsCIPEnabled
        {
            get { return ApplicationInstance.IsCIPEnabled; }
        }

        internal string InternalLanguageCode
        {
            get { return ApplicationInstance.LanguageCode; }
        }

        internal string InternalLanguageName
        {
            get { return ApplicationInstance.LanguageName; }
        }

        internal InvInvLanguageTools InternalLanguageTools
        {
            get { return InvInvLanguageTools.ByInvInvLanguageTools(ApplicationInstance.LanguageTools); }
        }


        internal int InternalLocale
        {
            get { return ApplicationInstance.Locale; }
        }

        internal int InternalMainFrameHWND
        {
            get { return ApplicationInstance.MainFrameHWND; }
        }

        internal InvInvMeasureTools InternalMeasureTools
        {
            get { return InvInvMeasureTools.ByInvInvMeasureTools(ApplicationInstance.MeasureTools); }
        }


        internal InvInvModelingEvents InternalModelingEvents
        {
            get { return InvInvModelingEvents.ByInvInvModelingEvents(ApplicationInstance.ModelingEvents); }
        }


        internal InvInvNotebookOptions InternalNotebookOptions
        {
            get { return InvInvNotebookOptions.ByInvInvNotebookOptions(ApplicationInstance.NotebookOptions); }
        }


        internal InvInvPartOptions InternalPartOptions
        {
            get { return InvInvPartOptions.ByInvInvPartOptions(ApplicationInstance.PartOptions); }
        }


        internal InvInvObjectsEnumeratorByVariant InternalPreferences
        {
            get { return InvInvObjectsEnumeratorByVariant.ByInvInvObjectsEnumeratorByVariant(ApplicationInstance.Preferences); }
        }


        internal InvInvPresentationOptions InternalPresentationOptions
        {
            get { return InvInvPresentationOptions.ByInvInvPresentationOptions(ApplicationInstance.PresentationOptions); }
        }


        internal bool InternalReady
        {
            get { return ApplicationInstance.Ready; }
        }

        internal InvInvReferenceKeyEvents InternalReferenceKeyEvents
        {
            get { return InvInvReferenceKeyEvents.ByInvInvReferenceKeyEvents(ApplicationInstance.ReferenceKeyEvents); }
        }


        internal InvInvRepresentationEvents InternalRepresentationEvents
        {
            get { return InvInvRepresentationEvents.ByInvInvRepresentationEvents(ApplicationInstance.RepresentationEvents); }
        }


        internal InvInvSaveOptions InternalSaveOptions
        {
            get { return InvInvSaveOptions.ByInvInvSaveOptions(ApplicationInstance.SaveOptions); }
        }


        internal InvInvSketch3DOptions InternalSketch3DOptions
        {
            get { return InvInvSketch3DOptions.ByInvInvSketch3DOptions(ApplicationInstance.Sketch3DOptions); }
        }


        internal InvInvSketchEvents InternalSketchEvents
        {
            get { return InvInvSketchEvents.ByInvInvSketchEvents(ApplicationInstance.SketchEvents); }
        }


        internal InvInvSketchOptions InternalSketchOptions
        {
            get { return InvInvSketchOptions.ByInvInvSketchOptions(ApplicationInstance.SketchOptions); }
        }


        internal InvInvSoftwareVersion InternalSoftwareVersion
        {
            get { return InvInvSoftwareVersion.ByInvInvSoftwareVersion(ApplicationInstance.SoftwareVersion); }
        }


        internal InvInvStyleEvents InternalStyleEvents
        {
            get { return InvInvStyleEvents.ByInvInvStyleEvents(ApplicationInstance.StyleEvents); }
        }


        internal InvInvStylesManager InternalStylesManager
        {
            get { return InvInvStylesManager.ByInvInvStylesManager(ApplicationInstance.StylesManager); }
        }


        internal InvInvStatusEnum InternalSubscriptionStatus
        {
            get { return InvInvStatusEnum.ByInvInvStatusEnum(ApplicationInstance.SubscriptionStatus); }
        }


        internal InvInvTestManager InternalTestManager
        {
            get { return InvInvTestManager.ByInvInvTestManager(ApplicationInstance.TestManager); }
        }


        internal InvInvTransactionManager InternalTransactionManager
        {
            get { return InvInvTransactionManager.ByInvInvTransactionManager(ApplicationInstance.TransactionManager); }
        }


        internal InvInvTransientBRep InternalTransientBRep
        {
            get { return InvInvTransientBRep.ByInvInvTransientBRep(ApplicationInstance.TransientBRep); }
        }


        internal InvInvTransientGeometry InternalTransientGeometry
        {
            get { return InvInvTransientGeometry.ByInvInvTransientGeometry(ApplicationInstance.TransientGeometry); }
        }


        internal InvInvTransientObjects InternalTransientObjects
        {
            get { return InvInvTransientObjects.ByInvInvTransientObjects(ApplicationInstance.TransientObjects); }
        }


        internal InvInvObjectTypeEnum InternalType
        {
            get { return InvInvObjectTypeEnum.ByInvInvObjectTypeEnum(ApplicationInstance.Type); }
        }


        internal InvInvUnitsOfMeasure InternalUnitsOfMeasure
        {
            get { return InvInvUnitsOfMeasure.ByInvInvUnitsOfMeasure(ApplicationInstance.UnitsOfMeasure); }
        }


        internal InvInvUserInterfaceManager InternalUserInterfaceManager
        {
            get { return InvInvUserInterfaceManager.ByInvInvUserInterfaceManager(ApplicationInstance.UserInterfaceManager); }
        }


        internal InvInvInventorVBAProjects InternalVBAProjects
        {
            get { return InvInvInventorVBAProjects.ByInvInvInventorVBAProjects(ApplicationInstance.VBAProjects); }
        }


        internal InvObject InternalVBE
        {
            get { return ApplicationInstance.VBE; }
        }

        internal InvInvViewsEnumerator InternalViews
        {
            get { return InvInvViewsEnumerator.ByInvInvViewsEnumerator(ApplicationInstance.Views); }
        }


        internal bool Internal_ForceMigrationOnOpen { get; set; }

        internal bool Internal_HarvestStylesOnOpen { get; set; }

        internal bool Internal_PurgeStylesOnOpen { get; set; }

        internal bool Internal_SuppressFileDirtyEvents { get; set; }

        internal InvObject Internal_TestIO { get; set; }

        internal bool Internal_TranscriptAPIChanges { get; set; }

        internal InvAssetLibrary InternalActiveAppearanceLibrary { get; set; }

        internal InvAssetLibrary InternalActiveMaterialLibrary { get; set; }

        internal bool InternalCameraRollMode3Dx { get; set; }

        internal string InternalCaption { get; set; }

        internal bool InternalFlythroughMode3Dx { get; set; }

        internal int InternalHeight { get; set; }

        internal int InternalLeft { get; set; }

        internal InvMaterialDisplayUnitsEnum InternalMaterialDisplayUnits { get; set; }

        internal bool InternalMRUDisplay { get; set; }

        internal bool InternalMRUEnabled { get; set; }

        internal bool InternalOpenDocumentsDisplay { get; set; }

        internal bool InternalScreenUpdating { get; set; }

        internal bool InternalSilentOperation { get; set; }

        internal string InternalStatusBarText { get; set; }

        internal bool InternalSupportsFileManagement { get; set; }

        internal int InternalTop { get; set; }

        internal string InternalUserName { get; set; }

        internal bool InternalVisible { get; set; }

        internal int InternalWidth { get; set; }

        internal InvWindowsSizeEnum InternalWindowState { get; set; }
        #endregion

        #region Private constructors
        private InvApplication(InvApplication invApplication)
        {
            InternalApplication = invApplication.InternalApplication;
        }

        private InvApplication(Inventor.Application invApplication)
        {
            InternalApplication = invApplication;
        }
        #endregion

        #region Private methods
        private void InternalConstructInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            ApplicationInstance.ConstructInternalNameAndRevisionId( internalNameToken,  revisionIdToken, out  internalName, out  revisionId);
        }

        private void InternalCreateFileDialog(out FileDialog dialog)
        {
            ApplicationInstance.CreateFileDialog(out  dialog);
        }

        private void InternalCreateInternalName(string name, string number, string custom, out string internalName)
        {
            ApplicationInstance.CreateInternalName( name,  number,  custom, out  internalName);
        }

        private InvProgressBar InternalCreateProgressBar(bool displayInStatusBar, int numberOfSteps, string title, bool allowCancel, int hWND)
        {
            return ApplicationInstance.CreateProgressBar( displayInStatusBar,  numberOfSteps,  title,  allowCancel,  hWND);
        }

        private void InternalGetAppFrameExtents(out int top, out int left, out int height, out int width)
        {
            ApplicationInstance.GetAppFrameExtents(out  top, out  left, out  height, out  width);
        }

        private InvObject InternalGetInterfaceObject(string progIDorCLSID)
        {
            return ApplicationInstance.GetInterfaceObject( progIDorCLSID);
        }

        private InvObject InternalGetInterfaceObject32(string progIDorCLSID)
        {
            return ApplicationInstance.GetInterfaceObject32( progIDorCLSID);
        }

        private string InternalGetTemplateFile(InvDocumentTypeEnum documentType, InvSystemOfMeasureEnum systemOfMeasure, InvDraftingStandardEnum draftingStandard, InvObject documentSubType)
        {
            return ApplicationInstance.GetTemplateFile( documentType,  systemOfMeasure,  draftingStandard,  documentSubType);
        }

        private string InternalLicenseChallenge()
        {
            return ApplicationInstance.LicenseChallenge();
        }

        private void InternalLicenseResponse(string response)
        {
            ApplicationInstance.LicenseResponse( response);
        }

        private bool InternalLogin()
        {
            return ApplicationInstance.Login();
        }

        private void InternalMove(int top, int left, int height, int width)
        {
            ApplicationInstance.Move( top,  left,  height,  width);
        }

        private void InternalQuit()
        {
            ApplicationInstance.Quit();
        }

        private void InternalReserveLicense(string clientId)
        {
            ApplicationInstance.ReserveLicense( clientId);
        }

        private void InternalUnreserveLicense(string clientId)
        {
            ApplicationInstance.UnreserveLicense( clientId);
        }

        private void InternalWriteCIPWaypoint(string waypointData)
        {
            ApplicationInstance.WriteCIPWaypoint( waypointData);
        }

        #endregion

        #region Public properties
        public Inventor.Application ApplicationInstance
        {
            get { return InternalApplication; }
            set { InternalApplication = value; }
        }

        public InvInv_AppPerformanceMonitor _AppPerformanceMonitor
        {
            get { return Internal_AppPerformanceMonitor; }
        }

        public bool _CanReplayTranscript
        {
            get { return Internal_CanReplayTranscript; }
        }

        public InvInvDebugInstrumentation _DebugInstrumentation
        {
            get { return Internal_DebugInstrumentation; }
        }

        public string _TranscriptFileName
        {
            get { return Internal_TranscriptFileName; }
        }

        public InvInvColorScheme ActiveColorScheme
        {
            get { return InternalActiveColorScheme; }
        }

        public InvInv_Document ActiveDocument
        {
            get { return InternalActiveDocument; }
        }

        public InvInvDocumentTypeEnum ActiveDocumentType
        {
            get { return InternalActiveDocumentType; }
        }

        public InvInv_Document ActiveEditDocument
        {
            get { return InternalActiveEditDocument; }
        }

        public InvObject ActiveEditObject
        {
            get { return InternalActiveEditObject; }
        }

        public InvInvEnvironment ActiveEnvironment
        {
            get { return InternalActiveEnvironment; }
        }

        public InvInvView ActiveView
        {
            get { return InternalActiveView; }
        }

        public string AllUsersAppDataPath
        {
            get { return InternalAllUsersAppDataPath; }
        }

        public InvInvApplicationAddIns ApplicationAddIns
        {
            get { return InternalApplicationAddIns; }
        }

        public InvInvApplicationEvents ApplicationEvents
        {
            get { return InternalApplicationEvents; }
        }

        public InvInvAssemblyEvents AssemblyEvents
        {
            get { return InternalAssemblyEvents; }
        }

        public InvInvAssemblyOptions AssemblyOptions
        {
            get { return InternalAssemblyOptions; }
        }

        public InvInvAssetLibraries AssetLibraries
        {
            get { return InternalAssetLibraries; }
        }

        public InvInvCameraEvents CameraEvents
        {
            get { return InternalCameraEvents; }
        }

        public InvInvChangeManager ChangeManager
        {
            get { return InternalChangeManager; }
        }

        public InvInvColorSchemes ColorSchemes
        {
            get { return InternalColorSchemes; }
        }

        public InvInvCommandManager CommandManager
        {
            get { return InternalCommandManager; }
        }

        public InvInvContentCenter ContentCenter
        {
            get { return InternalContentCenter; }
        }

        public InvInvContentCenterOptions ContentCenterOptions
        {
            get { return InternalContentCenterOptions; }
        }

        public string CurrentUserAppDataPath
        {
            get { return InternalCurrentUserAppDataPath; }
        }

        public InvInvDesignProjectManager DesignProjectManager
        {
            get { return InternalDesignProjectManager; }
        }

        public InvInvDisplayOptions DisplayOptions
        {
            get { return InternalDisplayOptions; }
        }

        public InvInvDocuments Documents
        {
            get { return InternalDocuments; }
        }

        public InvInvDrawingOptions DrawingOptions
        {
            get { return InternalDrawingOptions; }
        }

        public InvInvEnvironmentBaseCollection EnvironmentBaseCollection
        {
            get { return InternalEnvironmentBaseCollection; }
        }

        public InvInvEnvironments Environments
        {
            get { return InternalEnvironments; }
        }

        public InvInvErrorManager ErrorManager
        {
            get { return InternalErrorManager; }
        }

        public InvInvAssetsEnumerator FavoriteAssets
        {
            get { return InternalFavoriteAssets; }
        }

        public InvInvFileAccessEvents FileAccessEvents
        {
            get { return InternalFileAccessEvents; }
        }

        public InvInvFileLocations FileLocations
        {
            get { return InternalFileLocations; }
        }

        public InvInvFileManager FileManager
        {
            get { return InternalFileManager; }
        }

        public InvInvFileOptions FileOptions
        {
            get { return InternalFileOptions; }
        }

        public InvInvFileUIEvents FileUIEvents
        {
            get { return InternalFileUIEvents; }
        }

        public InvInvGeneralOptions GeneralOptions
        {
            get { return InternalGeneralOptions; }
        }

        public InvInvHardwareOptions HardwareOptions
        {
            get { return InternalHardwareOptions; }
        }

        public InvInvHelpManager HelpManager
        {
            get { return InternalHelpManager; }
        }

        public InvInviFeatureOptions iFeatureOptions
        {
            get { return InternaliFeatureOptions; }
        }

        public string InstallPath
        {
            get { return InternalInstallPath; }
        }

        public bool IsCIPEnabled
        {
            get { return InternalIsCIPEnabled; }
        }

        public string LanguageCode
        {
            get { return InternalLanguageCode; }
        }

        public string LanguageName
        {
            get { return InternalLanguageName; }
        }

        public InvInvLanguageTools LanguageTools
        {
            get { return InternalLanguageTools; }
        }

        public int Locale
        {
            get { return InternalLocale; }
        }

        public int MainFrameHWND
        {
            get { return InternalMainFrameHWND; }
        }

        public InvInvMeasureTools MeasureTools
        {
            get { return InternalMeasureTools; }
        }

        public InvInvModelingEvents ModelingEvents
        {
            get { return InternalModelingEvents; }
        }

        public InvInvNotebookOptions NotebookOptions
        {
            get { return InternalNotebookOptions; }
        }

        public InvInvPartOptions PartOptions
        {
            get { return InternalPartOptions; }
        }

        public InvInvObjectsEnumeratorByVariant Preferences
        {
            get { return InternalPreferences; }
        }

        public InvInvPresentationOptions PresentationOptions
        {
            get { return InternalPresentationOptions; }
        }

        public bool Ready
        {
            get { return InternalReady; }
        }

        public InvInvReferenceKeyEvents ReferenceKeyEvents
        {
            get { return InternalReferenceKeyEvents; }
        }

        public InvInvRepresentationEvents RepresentationEvents
        {
            get { return InternalRepresentationEvents; }
        }

        public InvInvSaveOptions SaveOptions
        {
            get { return InternalSaveOptions; }
        }

        public InvInvSketch3DOptions Sketch3DOptions
        {
            get { return InternalSketch3DOptions; }
        }

        public InvInvSketchEvents SketchEvents
        {
            get { return InternalSketchEvents; }
        }

        public InvInvSketchOptions SketchOptions
        {
            get { return InternalSketchOptions; }
        }

        public InvInvSoftwareVersion SoftwareVersion
        {
            get { return InternalSoftwareVersion; }
        }

        public InvInvStyleEvents StyleEvents
        {
            get { return InternalStyleEvents; }
        }

        public InvInvStylesManager StylesManager
        {
            get { return InternalStylesManager; }
        }

        public InvInvStatusEnum SubscriptionStatus
        {
            get { return InternalSubscriptionStatus; }
        }

        public InvInvTestManager TestManager
        {
            get { return InternalTestManager; }
        }

        public InvInvTransactionManager TransactionManager
        {
            get { return InternalTransactionManager; }
        }

        public InvInvTransientBRep TransientBRep
        {
            get { return InternalTransientBRep; }
        }

        public InvInvTransientGeometry TransientGeometry
        {
            get { return InternalTransientGeometry; }
        }

        public InvInvTransientObjects TransientObjects
        {
            get { return InternalTransientObjects; }
        }

        public InvInvObjectTypeEnum Type
        {
            get { return InternalType; }
        }

        public InvInvUnitsOfMeasure UnitsOfMeasure
        {
            get { return InternalUnitsOfMeasure; }
        }

        public InvInvUserInterfaceManager UserInterfaceManager
        {
            get { return InternalUserInterfaceManager; }
        }

        public InvInvInventorVBAProjects VBAProjects
        {
            get { return InternalVBAProjects; }
        }

        public InvObject VBE
        {
            get { return InternalVBE; }
        }

        public InvInvViewsEnumerator Views
        {
            get { return InternalViews; }
        }

        public Invbool _ForceMigrationOnOpen
        {
            get { return Internal_ForceMigrationOnOpen; }
            set { Internal_ForceMigrationOnOpen = value; }
        }

        public Invbool _HarvestStylesOnOpen
        {
            get { return Internal_HarvestStylesOnOpen; }
            set { Internal_HarvestStylesOnOpen = value; }
        }

        public Invbool _PurgeStylesOnOpen
        {
            get { return Internal_PurgeStylesOnOpen; }
            set { Internal_PurgeStylesOnOpen = value; }
        }

        public Invbool _SuppressFileDirtyEvents
        {
            get { return Internal_SuppressFileDirtyEvents; }
            set { Internal_SuppressFileDirtyEvents = value; }
        }

        public InvInvObject _TestIO
        {
            get { return Internal_TestIO; }
            set { Internal_TestIO = value; }
        }

        public Invbool _TranscriptAPIChanges
        {
            get { return Internal_TranscriptAPIChanges; }
            set { Internal_TranscriptAPIChanges = value; }
        }

        public InvInvAssetLibrary ActiveAppearanceLibrary
        {
            get { return InternalActiveAppearanceLibrary; }
            set { InternalActiveAppearanceLibrary = value; }
        }

        public InvInvAssetLibrary ActiveMaterialLibrary
        {
            get { return InternalActiveMaterialLibrary; }
            set { InternalActiveMaterialLibrary = value; }
        }

        public Invbool CameraRollMode3Dx
        {
            get { return InternalCameraRollMode3Dx; }
            set { InternalCameraRollMode3Dx = value; }
        }

        public Invstring Caption
        {
            get { return InternalCaption; }
            set { InternalCaption = value; }
        }

        public Invbool FlythroughMode3Dx
        {
            get { return InternalFlythroughMode3Dx; }
            set { InternalFlythroughMode3Dx = value; }
        }

        public Invint Height
        {
            get { return InternalHeight; }
            set { InternalHeight = value; }
        }

        public Invint Left
        {
            get { return InternalLeft; }
            set { InternalLeft = value; }
        }

        public InvInvMaterialDisplayUnitsEnum MaterialDisplayUnits
        {
            get { return InternalMaterialDisplayUnits; }
            set { InternalMaterialDisplayUnits = value; }
        }

        public Invbool MRUDisplay
        {
            get { return InternalMRUDisplay; }
            set { InternalMRUDisplay = value; }
        }

        public Invbool MRUEnabled
        {
            get { return InternalMRUEnabled; }
            set { InternalMRUEnabled = value; }
        }

        public Invbool OpenDocumentsDisplay
        {
            get { return InternalOpenDocumentsDisplay; }
            set { InternalOpenDocumentsDisplay = value; }
        }

        public Invbool ScreenUpdating
        {
            get { return InternalScreenUpdating; }
            set { InternalScreenUpdating = value; }
        }

        public Invbool SilentOperation
        {
            get { return InternalSilentOperation; }
            set { InternalSilentOperation = value; }
        }

        public Invstring StatusBarText
        {
            get { return InternalStatusBarText; }
            set { InternalStatusBarText = value; }
        }

        public Invbool SupportsFileManagement
        {
            get { return InternalSupportsFileManagement; }
            set { InternalSupportsFileManagement = value; }
        }

        public Invint Top
        {
            get { return InternalTop; }
            set { InternalTop = value; }
        }

        public Invstring UserName
        {
            get { return InternalUserName; }
            set { InternalUserName = value; }
        }

        public Invbool Visible
        {
            get { return InternalVisible; }
            set { InternalVisible = value; }
        }

        public Invint Width
        {
            get { return InternalWidth; }
            set { InternalWidth = value; }
        }

        public InvInvWindowsSizeEnum WindowState
        {
            get { return InternalWindowState; }
            set { InternalWindowState = value; }
        }

        #endregion
        #region Public static constructors
        public static InvApplication ByInvApplication(InvApplication invApplication)
        {
            return new InvApplication(invApplication);
        }
        public static InvApplication ByInvApplication(Inventor.Application invApplication)
        {
            return new InvApplication(invApplication);
        }
        #endregion

        #region Public methods
        public void ConstructInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            InternalConstructInternalNameAndRevisionId( internalNameToken,  revisionIdToken, out  internalName, out  revisionId);
        }

        public void CreateFileDialog(out FileDialog dialog)
        {
            InternalCreateFileDialog(out  dialog);
        }

        public void CreateInternalName(string name, string number, string custom, out string internalName)
        {
            InternalCreateInternalName( name,  number,  custom, out  internalName);
        }

        public InvProgressBar CreateProgressBar(bool displayInStatusBar, int numberOfSteps, string title, bool allowCancel, int hWND)
        {
            return InternalCreateProgressBar( displayInStatusBar,  numberOfSteps,  title,  allowCancel,  hWND);
        }

        public void GetAppFrameExtents(out int top, out int left, out int height, out int width)
        {
            InternalGetAppFrameExtents(out  top, out  left, out  height, out  width);
        }

        public InvObject GetInterfaceObject(string progIDorCLSID)
        {
            return InternalGetInterfaceObject( progIDorCLSID);
        }

        public InvObject GetInterfaceObject32(string progIDorCLSID)
        {
            return InternalGetInterfaceObject32( progIDorCLSID);
        }

        public string GetTemplateFile(InvDocumentTypeEnum documentType, InvSystemOfMeasureEnum systemOfMeasure, InvDraftingStandardEnum draftingStandard, InvObject documentSubType)
        {
            return InternalGetTemplateFile( documentType,  systemOfMeasure,  draftingStandard,  documentSubType);
        }

        public string LicenseChallenge()
        {
            return InternalLicenseChallenge();
        }

        public void LicenseResponse(string response)
        {
            InternalLicenseResponse( response);
        }

        public bool Login()
        {
            return InternalLogin();
        }

        public void Move(int top, int left, int height, int width)
        {
            InternalMove( top,  left,  height,  width);
        }

        public void Quit()
        {
            InternalQuit();
        }

        public void ReserveLicense(string clientId)
        {
            InternalReserveLicense( clientId);
        }

        public void UnreserveLicense(string clientId)
        {
            InternalUnreserveLicense( clientId);
        }

        public void WriteCIPWaypoint(string waypointData)
        {
            InternalWriteCIPWaypoint( waypointData);
        }

        #endregion
    }
}
