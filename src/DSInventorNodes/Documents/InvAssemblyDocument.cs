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
    public class InvAssemblyDocument
    {
        #region Internal properties
        internal Inventor.AssemblyDocument InternalAssemblyDocument { get; set; }

        internal int Internal_ComatoseNodesCount
        {
            get { return AssemblyDocumentInstance._ComatoseNodesCount; }
        }

        internal CommandIDEnum Internal_DefaultCommand
        {
            get { return AssemblyDocumentInstance._DefaultCommand; }
        }

        internal _DocPerformanceMonitor Internal_DocPerformanceMonitor
        {
            get { return AssemblyDocumentInstance._DocPerformanceMonitor; }
        }

        internal string Internal_InternalName
        {
            get { return AssemblyDocumentInstance._InternalName; }
        }

        internal string Internal_PrimaryDeselGUID
        {
            get { return AssemblyDocumentInstance._PrimaryDeselGUID; }
        }

        internal int Internal_SickNodesCount
        {
            get { return AssemblyDocumentInstance._SickNodesCount; }
        }

        internal Object InternalActivatedObject
        {
            get { return AssemblyDocumentInstance.ActivatedObject; }
        }

        internal DocumentsEnumerator InternalAllReferencedDocuments
        {
            get { return AssemblyDocumentInstance.AllReferencedDocuments; }
        }

        internal AssetsEnumerator InternalAppearanceAssets
        {
            get { return AssemblyDocumentInstance.AppearanceAssets; }
        }

        internal Assets InternalAssets
        {
            get { return AssemblyDocumentInstance.Assets; }
        }

        internal AttributeManager InternalAttributeManager
        {
            get { return AssemblyDocumentInstance.AttributeManager; }
        }

        internal AttributeSets InternalAttributeSets
        {
            get { return AssemblyDocumentInstance.AttributeSets; }
        }

        internal BrowserPanes InternalBrowserPanes
        {
            get { return AssemblyDocumentInstance.BrowserPanes; }
        }

        internal CachedGraphicsStatusEnum InternalCachedGraphicsStatus
        {
            get { return AssemblyDocumentInstance.CachedGraphicsStatus; }
        }

        internal bool InternalCompacted
        {
            get { return AssemblyDocumentInstance.Compacted; }
        }

        internal AssemblyComponentDefinition InternalComponentDefinition
        {
            get { return AssemblyDocumentInstance.ComponentDefinition; }
        }

        internal AssemblyComponentDefinitions InternalComponentDefinitions
        {
            get { return AssemblyDocumentInstance.ComponentDefinitions; }
        }

        internal string InternalDatabaseRevisionId
        {
            get { return AssemblyDocumentInstance.DatabaseRevisionId; }
        }

        internal string InternalDefaultCommand
        {
            get { return AssemblyDocumentInstance.DefaultCommand; }
        }

        internal string InternalDesignViewInfo
        {
            get { return AssemblyDocumentInstance.DesignViewInfo; }
        }

        internal DisabledCommandList InternalDisabledCommandList
        {
            get { return AssemblyDocumentInstance.DisabledCommandList; }
        }

        internal DisplaySettings InternalDisplaySettings
        {
            get { return AssemblyDocumentInstance.DisplaySettings; }
        }

        internal DocumentEvents InternalDocumentEvents
        {
            get { return AssemblyDocumentInstance.DocumentEvents; }
        }

        internal DocumentInterests InternalDocumentInterests
        {
            get { return AssemblyDocumentInstance.DocumentInterests; }
        }

        internal DocumentSubType InternalDocumentSubType
        {
            get { return AssemblyDocumentInstance.DocumentSubType; }
        }

        internal DocumentTypeEnum InternalDocumentType
        {
            get { return AssemblyDocumentInstance.DocumentType; }
        }

        internal EnvironmentManager InternalEnvironmentManager
        {
            get { return AssemblyDocumentInstance.EnvironmentManager; }
        }

        internal File InternalFile
        {
            get { return AssemblyDocumentInstance.File; }
        }

        internal int InternalFileSaveCounter
        {
            get { return AssemblyDocumentInstance.FileSaveCounter; }
        }

        internal string InternalFullDocumentName
        {
            get { return AssemblyDocumentInstance.FullDocumentName; }
        }

        internal GraphicsDataSetsCollection InternalGraphicsDataSetsCollection
        {
            get { return AssemblyDocumentInstance.GraphicsDataSetsCollection; }
        }

        internal HighlightSets InternalHighlightSets
        {
            get { return AssemblyDocumentInstance.HighlightSets; }
        }

        internal string InternalInternalName
        {
            get { return AssemblyDocumentInstance.InternalName; }
        }

        internal _Document InternalInventorDocument
        {
            get { return AssemblyDocumentInstance.InventorDocument; }
        }

        internal bool InternalIsModifiable
        {
            get { return AssemblyDocumentInstance.IsModifiable; }
        }

        internal string InternalLevelOfDetailName
        {
            get { return AssemblyDocumentInstance.LevelOfDetailName; }
        }

        internal LightingStyles InternalLightingStyles
        {
            get { return AssemblyDocumentInstance.LightingStyles; }
        }

        internal AssetsEnumerator InternalMaterialAssets
        {
            get { return AssemblyDocumentInstance.MaterialAssets; }
        }

        internal Materials InternalMaterials
        {
            get { return AssemblyDocumentInstance.Materials; }
        }

        internal ModelingSettings InternalModelingSettings
        {
            get { return AssemblyDocumentInstance.ModelingSettings; }
        }

        internal bool InternalNeedsMigrating
        {
            get { return AssemblyDocumentInstance.NeedsMigrating; }
        }

        internal ObjectVisibility InternalObjectVisibility
        {
            get { return AssemblyDocumentInstance.ObjectVisibility; }
        }

        internal bool InternalOpen
        {
            get { return AssemblyDocumentInstance.Open; }
        }

        internal FileOwnershipEnum InternalOwnershipType
        {
            get { return AssemblyDocumentInstance.OwnershipType; }
        }

        internal Object InternalParent
        {
            get { return AssemblyDocumentInstance.Parent; }
        }

        internal AssetsEnumerator InternalPhysicalAssets
        {
            get { return AssemblyDocumentInstance.PhysicalAssets; }
        }

        internal PrintManager InternalPrintManager
        {
            get { return AssemblyDocumentInstance.PrintManager; }
        }

        internal PropertySets InternalPropertySets
        {
            get { return AssemblyDocumentInstance.PropertySets; }
        }

        internal CommandTypesEnum InternalRecentChanges
        {
            get { return AssemblyDocumentInstance.RecentChanges; }
        }

        internal DocumentDescriptorsEnumerator InternalReferencedDocumentDescriptors
        {
            get { return AssemblyDocumentInstance.ReferencedDocumentDescriptors; }
        }

        internal DocumentsEnumerator InternalReferencedDocuments
        {
            get { return AssemblyDocumentInstance.ReferencedDocuments; }
        }

        internal ReferencedFileDescriptors InternalReferencedFileDescriptors
        {
            get { return AssemblyDocumentInstance.ReferencedFileDescriptors; }
        }

        internal DocumentsEnumerator InternalReferencedFiles
        {
            get { return AssemblyDocumentInstance.ReferencedFiles; }
        }

        internal ReferencedOLEFileDescriptors InternalReferencedOLEFileDescriptors
        {
            get { return AssemblyDocumentInstance.ReferencedOLEFileDescriptors; }
        }

        internal ReferenceKeyManager InternalReferenceKeyManager
        {
            get { return AssemblyDocumentInstance.ReferenceKeyManager; }
        }

        internal DocumentsEnumerator InternalReferencingDocuments
        {
            get { return AssemblyDocumentInstance.ReferencingDocuments; }
        }

        internal RenderStyles InternalRenderStyles
        {
            get { return AssemblyDocumentInstance.RenderStyles; }
        }

        internal bool InternalRequiresUpdate
        {
            get { return AssemblyDocumentInstance.RequiresUpdate; }
        }

        internal bool InternalReservedForWrite
        {
            get { return AssemblyDocumentInstance.ReservedForWrite; }
        }

        internal string InternalReservedForWriteLogin
        {
            get { return AssemblyDocumentInstance.ReservedForWriteLogin; }
        }

        internal string InternalReservedForWriteName
        {
            get { return AssemblyDocumentInstance.ReservedForWriteName; }
        }

        internal DateTime InternalReservedForWriteTime
        {
            get { return AssemblyDocumentInstance.ReservedForWriteTime; }
        }

        internal int InternalReservedForWriteVersion
        {
            get { return AssemblyDocumentInstance.ReservedForWriteVersion; }
        }

        internal string InternalRevisionId
        {
            get { return AssemblyDocumentInstance.RevisionId; }
        }

        internal SelectSet InternalSelectSet
        {
            get { return AssemblyDocumentInstance.SelectSet; }
        }

        internal SketchSettings InternalSketchSettings
        {
            get { return AssemblyDocumentInstance.SketchSettings; }
        }

        internal SoftwareVersion InternalSoftwareVersionCreated
        {
            get { return AssemblyDocumentInstance.SoftwareVersionCreated; }
        }

        internal SoftwareVersion InternalSoftwareVersionSaved
        {
            get { return AssemblyDocumentInstance.SoftwareVersionSaved; }
        }

        internal IPictureDisp InternalThumbnail
        {
            get { return (IPictureDisp)AssemblyDocumentInstance.Thumbnail; }
        }

        internal ThumbnailSaveOptionEnum InternalThumbnailSaveOption
        {
            get { return AssemblyDocumentInstance.ThumbnailSaveOption; }
        }

        internal OGSSceneNode InternalTopOGSSceneNode
        {
            get { return AssemblyDocumentInstance.TopOGSSceneNode; }
        }

        internal ObjectTypeEnum InternalType
        {
            get { return AssemblyDocumentInstance.Type; }
        }

        internal UnitsOfMeasure InternalUnitsOfMeasure
        {
            get { return AssemblyDocumentInstance.UnitsOfMeasure; }
        }

        internal InventorVBAProject InternalVBAProject
        {
            get { return AssemblyDocumentInstance.VBAProject; }
        }

        internal Views InternalViews
        {
            get { return AssemblyDocumentInstance.Views; }
        }

        internal bool Internal_ExcludeFromBOM { get; set; }

        internal LightingStyle InternalActiveLightingStyle { get; set; }

        internal bool InternalDirty { get; set; }

        internal CommandTypesEnum InternalDisabledCommandTypes { get; set; }

        internal string InternalDisplayName { get; set; }

        internal bool InternalDisplayNameOverridden { get; set; }

        internal string InternalFullFileName { get; set; }

        internal bool InternalIsOpenExpress { get; set; }

        internal bool InternalIsOpenLightweight { get; set; }

        internal bool InternalReservedForWriteByMe { get; set; }

        internal SelectionPriorityEnum InternalSelectionPriority { get; set; }

        internal string InternalSubType { get; set; }
        #endregion

        #region Private constructors
        private InvAssemblyDocument(InvAssemblyDocument invAssemblyDocument)
        {
            InternalAssemblyDocument = invAssemblyDocument.InternalAssemblyDocument;
        }

        private InvAssemblyDocument(Inventor.AssemblyDocument invAssemblyDocument)
        {
            InternalAssemblyDocument = invAssemblyDocument;
        }
        #endregion

        #region Private methods
        private void InternalActivate()
        {
            AssemblyDocumentInstance.Activate();
        }

        private void InternalClose(bool skipSave)
        {
            AssemblyDocumentInstance.Close( skipSave);
        }

        private HighlightSet InternalCreateHighlightSet()
        {
            return AssemblyDocumentInstance.CreateHighlightSet();
        }

        private DocumentsEnumerator InternalFindWhereUsed(string fullFileName)
        {
            return AssemblyDocumentInstance.FindWhereUsed( fullFileName);
        }

        private void InternalGetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        {
            AssemblyDocumentInstance.GetLocationFoundIn(out  locationName, out  locationType);
        }

        private void InternalGetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out ObjectCollection disabledCommands)
        {
            AssemblyDocumentInstance.GetMissingAddInBehavior(out  clientId, out  disabledCommandTypesEnum, out  disabledCommands);
        }

        private Object InternalGetPrivateStorage(string storageName, bool createIfNecessary)
        {
            return AssemblyDocumentInstance.GetPrivateStorage( storageName,  createIfNecessary);
        }

        private Object InternalGetPrivateStream(string streamName, bool createIfNecessary)
        {
            return AssemblyDocumentInstance.GetPrivateStream( streamName,  createIfNecessary);
        }

        private void InternalGetSelectedObject(GenericObject selection, out ObjectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, ref Object selectedObject)
        {
            AssemblyDocumentInstance.GetSelectedObject( selection, out  objectType, out  additionalData, out  containingOccurrence, ref  selectedObject);
        }

        private bool InternalHasPrivateStorage(string storageName)
        {
            return AssemblyDocumentInstance.HasPrivateStorage( storageName);
        }

        private bool InternalHasPrivateStream(string streamName)
        {
            return AssemblyDocumentInstance.HasPrivateStream( streamName);
        }

        private void InternalLockSaveSet()
        {
            AssemblyDocumentInstance.LockSaveSet();
        }

        private void InternalMigrate()
        {
            AssemblyDocumentInstance.Migrate();
        }

        private void InternalPutInternalName(string name, string number, string custom, out string internalName)
        {
            AssemblyDocumentInstance.PutInternalName( name,  number,  custom, out  internalName);
        }

        private void InternalPutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            AssemblyDocumentInstance.PutInternalNameAndRevisionId( internalNameToken,  revisionIdToken, out  internalName, out  revisionId);
        }

        private void InternalRebuild()
        {
            AssemblyDocumentInstance.Rebuild();
        }

        private bool InternalRebuild2(bool acceptErrorsAndContinue)
        {
            return AssemblyDocumentInstance.Rebuild2( acceptErrorsAndContinue);
        }

        private void InternalReleaseReference()
        {
            AssemblyDocumentInstance.ReleaseReference();
        }

        private void InternalRevertReservedForWriteByMe()
        {
            AssemblyDocumentInstance.RevertReservedForWriteByMe();
        }

        private void InternalSave()
        {
            AssemblyDocumentInstance.Save();
        }

        private void InternalSave2(bool saveDependents, Object documentsToSave)
        {
            AssemblyDocumentInstance.Save2( saveDependents,  documentsToSave);
        }

        private void InternalSaveAs(string fileName, bool saveCopyAs)
        {
            AssemblyDocumentInstance.SaveAs( fileName,  saveCopyAs);
        }

        private void InternalSetMissingAddInBehavior(string clientId, CommandTypesEnum disabledCommandTypesEnum, Object disabledCommands)
        {
            AssemblyDocumentInstance.SetMissingAddInBehavior( clientId,  disabledCommandTypesEnum,  disabledCommands);
        }

        private void InternalSetThumbnailSaveOption(ThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        {
            AssemblyDocumentInstance.SetThumbnailSaveOption( saveOption,  imageFullFileName);
        }

        private void InternalUpdate()
        {
            AssemblyDocumentInstance.Update();
        }

        private bool InternalUpdate2(bool acceptErrorsAndContinue)
        {
            return AssemblyDocumentInstance.Update2( acceptErrorsAndContinue);
        }

        #endregion

        #region Public properties
        public Inventor.AssemblyDocument AssemblyDocumentInstance
        {
            get { return InternalAssemblyDocument; }
            set { InternalAssemblyDocument = value; }
        }
        #endregion

        #region Public static constructors
        public static InvAssemblyDocument ByInvAssemblyDocument(InvAssemblyDocument invAssemblyDocument)
        {
            return new InvAssemblyDocument(invAssemblyDocument);
        }

        public static InvAssemblyDocument ByInvAssemblyDocument(Inventor.AssemblyDocument invAssemblyDocument)
        {
            return new InvAssemblyDocument(invAssemblyDocument);
        }
        #endregion

        #region Public methods
        public void Activate()
        {
            InternalActivate();
        }

        public void Close(bool skipSave)
        {
            InternalClose( skipSave);
        }

        public HighlightSet CreateHighlightSet()
        {
            return InternalCreateHighlightSet();
        }

        public DocumentsEnumerator FindWhereUsed(string fullFileName)
        {
            return InternalFindWhereUsed( fullFileName);
        }

        public void GetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        {
            InternalGetLocationFoundIn(out  locationName, out  locationType);
        }

        public void GetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out ObjectCollection disabledCommands)
        {
            InternalGetMissingAddInBehavior(out  clientId, out  disabledCommandTypesEnum, out  disabledCommands);
        }

        public Object GetPrivateStorage(string storageName, bool createIfNecessary)
        {
            return InternalGetPrivateStorage( storageName,  createIfNecessary);
        }

        public Object GetPrivateStream(string streamName, bool createIfNecessary)
        {
            return InternalGetPrivateStream( streamName,  createIfNecessary);
        }

        public void GetSelectedObject(GenericObject selection, out ObjectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, ref Object selectedObject)
        {
            InternalGetSelectedObject( selection, out  objectType, out  additionalData, out  containingOccurrence, ref  selectedObject);
        }

        public bool HasPrivateStorage(string storageName)
        {
            return InternalHasPrivateStorage( storageName);
        }

        public bool HasPrivateStream(string streamName)
        {
            return InternalHasPrivateStream( streamName);
        }

        public void LockSaveSet()
        {
            InternalLockSaveSet();
        }

        public void Migrate()
        {
            InternalMigrate();
        }

        public void PutInternalName(string name, string number, string custom, out string internalName)
        {
            InternalPutInternalName( name,  number,  custom, out  internalName);
        }

        public void PutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            InternalPutInternalNameAndRevisionId( internalNameToken,  revisionIdToken, out  internalName, out  revisionId);
        }

        public void Rebuild()
        {
            InternalRebuild();
        }

        public bool Rebuild2(bool acceptErrorsAndContinue)
        {
            return InternalRebuild2( acceptErrorsAndContinue);
        }

        public void ReleaseReference()
        {
            InternalReleaseReference();
        }

        public void RevertReservedForWriteByMe()
        {
            InternalRevertReservedForWriteByMe();
        }

        public void Save()
        {
            InternalSave();
        }

        public void Save2(bool saveDependents, Object documentsToSave)
        {
            InternalSave2( saveDependents,  documentsToSave);
        }

        public void SaveAs(string fileName, bool saveCopyAs)
        {
            InternalSaveAs( fileName,  saveCopyAs);
        }

        public void SetMissingAddInBehavior(string clientId, CommandTypesEnum disabledCommandTypesEnum, Object disabledCommands)
        {
            InternalSetMissingAddInBehavior( clientId,  disabledCommandTypesEnum,  disabledCommands);
        }

        public void SetThumbnailSaveOption(ThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        {
            InternalSetThumbnailSaveOption( saveOption,  imageFullFileName);
        }

        public void Update()
        {
            InternalUpdate();
        }

        public bool Update2(bool acceptErrorsAndContinue)
        {
            return InternalUpdate2( acceptErrorsAndContinue);
        }

        #endregion
    }
}
