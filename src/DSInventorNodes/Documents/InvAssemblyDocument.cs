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

        internal int Internal_ComatoseNodesCount { get; }

        internal CommandIDEnum Internal_DefaultCommand { get; }

        internal _DocPerformanceMonitor Internal_DocPerformanceMonitor { get; }

        internal string Internal_InternalName { get; }

        internal bool Internal_IsTemplateUsed { get; }

        internal string Internal_PrimaryDeselGUID { get; }

        internal int Internal_SickNodesCount { get; }

        internal Object InternalActivatedObject { get; }

        internal DocumentsEnumerator InternalAllReferencedDocuments { get; }

        internal AssetsEnumerator InternalAppearanceAssets { get; }

        internal Assets InternalAssets { get; }

        internal AttributeManager InternalAttributeManager { get; }

        internal AttributeSets InternalAttributeSets { get; }

        internal BrowserPanes InternalBrowserPanes { get; }

        internal CachedGraphicsStatusEnum InternalCachedGraphicsStatus { get; }

        internal bool InternalCompacted { get; }

        internal AssemblyComponentDefinition InternalComponentDefinition { get; }

        internal AssemblyComponentDefinitions InternalComponentDefinitions { get; }

        internal string InternalDatabaseRevisionId { get; }

        internal string InternalDefaultCommand { get; }

        internal string InternalDesignViewInfo { get; }

        internal DisabledCommandList InternalDisabledCommandList { get; }

        internal DisplaySettings InternalDisplaySettings { get; }

        internal DocumentEvents InternalDocumentEvents { get; }

        internal DocumentInterests InternalDocumentInterests { get; }

        internal DocumentSubType InternalDocumentSubType { get; }

        internal DocumentTypeEnum InternalDocumentType { get; }

        internal EnvironmentManager InternalEnvironmentManager { get; }

        internal File InternalFile { get; }

        internal int InternalFileSaveCounter { get; }

        internal string InternalFullDocumentName { get; }

        internal GraphicsDataSetsCollection InternalGraphicsDataSetsCollection { get; }

        internal HighlightSets InternalHighlightSets { get; }

        internal string InternalInternalName { get; }

        internal _Document InternalInventorDocument { get; }

        internal bool InternalIsModifiable { get; }

        internal string InternalLevelOfDetailName { get; }

        internal LightingStyles InternalLightingStyles { get; }

        internal AssetsEnumerator InternalMaterialAssets { get; }

        internal Materials InternalMaterials { get; }

        internal ModelingSettings InternalModelingSettings { get; }

        internal bool InternalNeedsMigrating { get; }

        internal ObjectVisibility InternalObjectVisibility { get; }

        internal bool InternalOpen { get; }

        internal FileOwnershipEnum InternalOwnershipType { get; }

        internal Object InternalParent { get; }

        internal AssetsEnumerator InternalPhysicalAssets { get; }

        internal PrintManager InternalPrintManager { get; }

        internal PropertySets InternalPropertySets { get; }

        internal CommandTypesEnum InternalRecentChanges { get; }

        internal DocumentDescriptorsEnumerator InternalReferencedDocumentDescriptors { get; }

        internal DocumentsEnumerator InternalReferencedDocuments { get; }

        internal ReferencedFileDescriptors InternalReferencedFileDescriptors { get; }

        internal DocumentsEnumerator InternalReferencedFiles { get; }

        internal ReferencedOLEFileDescriptors InternalReferencedOLEFileDescriptors { get; }

        internal ObjectsEnumerator InternalReferencedOLEFileDescriptors2 { get; }

        internal ReferenceKeyManager InternalReferenceKeyManager { get; }

        internal DocumentsEnumerator InternalReferencingDocuments { get; }

        internal RenderStyles InternalRenderStyles { get; }

        internal bool InternalRequiresUpdate { get; }

        internal bool InternalReservedForWrite { get; }

        internal string InternalReservedForWriteLogin { get; }

        internal string InternalReservedForWriteName { get; }

        internal DateTime InternalReservedForWriteTime { get; }

        internal int InternalReservedForWriteVersion { get; }

        internal string InternalRevisionId { get; }

        internal SelectSet InternalSelectSet { get; }

        internal SketchSettings InternalSketchSettings { get; }

        internal SoftwareVersion InternalSoftwareVersionCreated { get; }

        internal SoftwareVersion InternalSoftwareVersionSaved { get; }

        internal IPictureDisp InternalThumbnail { get; }

        internal ThumbnailSaveOptionEnum InternalThumbnailSaveOption { get; }

        internal OGSSceneNode InternalTopOGSSceneNode { get; }

        internal ObjectTypeEnum InternalType { get; }

        internal UnitsOfMeasure InternalUnitsOfMeasure { get; }

        internal InventorVBAProject InternalVBAProject { get; }

        internal Views InternalViews { get; }

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
