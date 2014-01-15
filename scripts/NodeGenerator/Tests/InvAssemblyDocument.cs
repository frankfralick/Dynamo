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
    class InvAssemblyDocument
    {
        #region Internal properties
        internal int Internal_ComatoseNodesCount { get; }

        internal CommandIDEnum Internal_DefaultCommand { get; }

        internal _DocPerformanceMonitor Internal_DocPerformanceMonitor { get; }

        internal bool Internal_ExcludeFromBOM { get; }

        internal string Internal_InternalName { get; }

        internal bool Internal_IsTemplateUsed { get; }

        internal string Internal_PrimaryDeselGUID { get; }

        internal int Internal_SickNodesCount { get; }

        internal object InternalActivatedObject { get; }

        internal LightingStyle InternalActiveLightingStyle { get; }

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

        internal bool InternalDirty { get; }

        internal DisabledCommandList InternalDisabledCommandList { get; }

        internal CommandTypesEnum InternalDisabledCommandTypes { get; }

        internal string InternalDisplayName { get; }

        internal bool InternalDisplayNameOverridden { get; }

        internal DisplaySettings InternalDisplaySettings { get; }

        internal DocumentEvents InternalDocumentEvents { get; }

        internal DocumentInterests InternalDocumentInterests { get; }

        internal DocumentSubType InternalDocumentSubType { get; }

        internal DocumentTypeEnum InternalDocumentType { get; }

        internal EnvironmentManager InternalEnvironmentManager { get; }

        internal File InternalFile { get; }

        internal int InternalFileSaveCounter { get; }

        internal string InternalFullDocumentName { get; }

        internal string InternalFullFileName { get; }

        internal GraphicsDataSetsCollection InternalGraphicsDataSetsCollection { get; }

        internal HighlightSets InternalHighlightSets { get; }

        internal string InternalInternalName { get; }

        internal _Document InternalInventorDocument { get; }

        internal bool InternalIsModifiable { get; }

        internal bool InternalIsOpenExpress { get; }

        internal bool InternalIsOpenLightweight { get; }

        internal string InternalLevelOfDetailName { get; }

        internal LightingStyles InternalLightingStyles { get; }

        internal AssetsEnumerator InternalMaterialAssets { get; }

        internal Materials InternalMaterials { get; }

        internal ModelingSettings InternalModelingSettings { get; }

        internal bool InternalNeedsMigrating { get; }

        internal objectVisibility InternalObjectVisibility { get; }

        internal bool InternalOpen { get; }

        internal FileOwnershipEnum InternalOwnershipType { get; }

        internal object InternalParent { get; }

        internal AssetsEnumerator InternalPhysicalAssets { get; }

        internal PrintManager InternalPrintManager { get; }

        internal PropertySets InternalPropertySets { get; }

        internal CommandTypesEnum InternalRecentChanges { get; }

        internal DocumentDescriptorsEnumerator InternalReferencedDocumentDescriptors { get; }

        internal DocumentsEnumerator InternalReferencedDocuments { get; }

        internal ReferencedFileDescriptors InternalReferencedFileDescriptors { get; }

        internal DocumentsEnumerator InternalReferencedFiles { get; }

        internal ReferencedOLEFileDescriptors InternalReferencedOLEFileDescriptors { get; }

        internal objectsEnumerator InternalReferencedOLEFileDescriptors2 { get; }

        internal ReferenceKeyManager InternalReferenceKeyManager { get; }

        internal DocumentsEnumerator InternalReferencingDocuments { get; }

        internal RenderStyles InternalRenderStyles { get; }

        internal bool InternalRequiresUpdate { get; }

        internal bool InternalReservedForWrite { get; }

        internal bool InternalReservedForWriteByMe { get; }

        internal string InternalReservedForWriteLogin { get; }

        internal string InternalReservedForWriteName { get; }

        internal DateTime InternalReservedForWriteTime { get; }

        internal int InternalReservedForWriteVersion { get; }

        internal string InternalRevisionId { get; }

        internal SelectionPriorityEnum InternalSelectionPriority { get; }

        internal SelectSet InternalSelectSet { get; }

        internal SketchSettings InternalSketchSettings { get; }

        internal SoftwareVersion InternalSoftwareVersionCreated { get; }

        internal SoftwareVersion InternalSoftwareVersionSaved { get; }

        internal string InternalSubType { get; }

        internal IPictureDisp InternalThumbnail { get; }

        internal ThumbnailSaveOptionEnum InternalThumbnailSaveOption { get; }

        internal OGSSceneNode InternalTopOGSSceneNode { get; }

        internal objectTypeEnum InternalType { get; }

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
        private InvAssemblyDocument(Inventor.AssemblyDocument assemblyDocument)
        {
            InternalAssemblyDocument = assemblyDocument;
        }
        #endregion

        #region Private methods
        private void _DeleteUnusedEmbeddings(bool preview, out int numEmbeddings, out string[] embeddings)
        {
            Inventor._DeleteUnusedEmbeddings(bool preview, out int numEmbeddings, out string[] embeddings)
        }

        private void _PutInternalNameAndFileVersion(string name, string number, string custom, string revision, out string internalName, out string fileVersion)
        {
            Inventor._PutInternalNameAndFileVersion(string name, string number, string custom, string revision, out string internalName, out string fileVersion)
        }

        private void _VBAProjectChanged()
        {
            Inventor._VBAProjectChanged()
        }

        private void _XmlOutToFile(string schemaXmlString, string outXmlFile)
        {
            Inventor._XmlOutToFile(string schemaXmlString, string outXmlFile)
        }

        private void Activate()
        {
            Inventor.Activate()
        }

        private void Close(bool skipSave)
        {
            Inventor.Close(bool skipSave)
        }

        private InvHighlightSet CreateHighlightSet()
        {
            return InvHighlightSet InternalCreateHighlightSet()
        }

        private InvDocumentsEnumerator FindWhereUsed(string fullFileName)
        {
            return InvDocumentsEnumerator InternalFindWhereUsed(string fullFileName)
        }

        private void GetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        {
            Inventor.GetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        }

        private void GetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out objectCollection disabledCommands)
        {
            Inventor.GetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out objectCollection disabledCommands)
        }

        private object GetPrivateStorage(string storageName, bool createIfNecessary)
        {
            return object InternalGetPrivateStorage(string storageName, bool createIfNecessary)
        }

        private object GetPrivateStream(string streamName, bool createIfNecessary)
        {
            return object InternalGetPrivateStream(string streamName, bool createIfNecessary)
        }

        private void GetSelectedObject(Genericobject selection, out objectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, out object selectedObject)
        {
            Inventor.GetSelectedObject(Genericobject selection, out objectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, out object selectedObject)
        }

        private bool HasPrivateStorage(string storageName)
        {
            return bool InternalHasPrivateStorage(string storageName)
        }

        private bool HasPrivateStream(string streamName)
        {
            return bool InternalHasPrivateStream(string streamName)
        }

        private void LockSaveSet()
        {
            Inventor.LockSaveSet()
        }

        private void Migrate()
        {
            Inventor.Migrate()
        }

        private void PutInternalName(string name, string number, string custom, out string internalName)
        {
            Inventor.PutInternalName(string name, string number, string custom, out string internalName)
        }

        private void PutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            Inventor.PutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        }

        private void Rebuild()
        {
            Inventor.Rebuild()
        }

        private bool Rebuild2(bool acceptErrorsAndContinue)
        {
            return bool InternalRebuild2(bool acceptErrorsAndContinue)
        }

        private void ReleaseReference()
        {
            Inventor.ReleaseReference()
        }

        private void RevertReservedForWriteByMe()
        {
            Inventor.RevertReservedForWriteByMe()
        }

        private void Save()
        {
            Inventor.Save()
        }

        private void Save2(bool saveDependents, object documentsToSave)
        {
            Inventor.Save2(bool saveDependents, object documentsToSave)
        }

        private void SaveAs(string fileName, bool saveCopyAs)
        {
            Inventor.SaveAs(string fileName, bool saveCopyAs)
        }

        private void SetMissingAddInBehavior(string clientId, InvCommandTypesEnum disabledCommandTypesEnum, object disabledCommands)
        {
            Inventor.SetMissingAddInBehavior(string clientId, InvCommandTypesEnum disabledCommandTypesEnum, object disabledCommands)
        }

        private void SetThumbnailSaveOption(InvThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        {
            Inventor.SetThumbnailSaveOption(InvThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        }

        private void Update()
        {
            Inventor.Update()
        }

        private bool Update2(bool acceptErrorsAndContinue)
        {
            return bool InternalUpdate2(bool acceptErrorsAndContinue)
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
            return new InvAssemblyDocument(invAssemblyDocument)
        }
        #endregion

        #region Public methods
        public void _DeleteUnusedEmbeddings(bool preview, out int numEmbeddings, out string[] embeddings)
        {
            Internal_DeleteUnusedEmbeddings(bool preview, out int numEmbeddings, out string[] embeddings)
        }

        public void _PutInternalNameAndFileVersion(string name, string number, string custom, string revision, out string internalName, out string fileVersion)
        {
            Internal_PutInternalNameAndFileVersion(string name, string number, string custom, string revision, out string internalName, out string fileVersion)
        }

        public void _VBAProjectChanged()
        {
            Internal_VBAProjectChanged()
        }

        public void _XmlOutToFile(string schemaXmlString, string outXmlFile)
        {
            Internal_XmlOutToFile(string schemaXmlString, string outXmlFile)
        }

        public void Activate()
        {
            InternalActivate()
        }

        public void Close(bool skipSave)
        {
            InternalClose(bool skipSave)
        }

        public InvHighlightSet CreateHighlightSet()
        {
            return InvHighlightSet InternalCreateHighlightSet()
        }

        public InvDocumentsEnumerator FindWhereUsed(string fullFileName)
        {
            return InvDocumentsEnumerator InternalFindWhereUsed(string fullFileName)
        }

        public void GetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        {
            InternalGetLocationFoundIn(out string locationName, out LocationTypeEnum locationType)
        }

        public void GetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out objectCollection disabledCommands)
        {
            InternalGetMissingAddInBehavior(out string clientId, out CommandTypesEnum disabledCommandTypesEnum, out objectCollection disabledCommands)
        }

        public object GetPrivateStorage(string storageName, bool createIfNecessary)
        {
            return object InternalGetPrivateStorage(string storageName, bool createIfNecessary)
        }

        public object GetPrivateStream(string streamName, bool createIfNecessary)
        {
            return object InternalGetPrivateStream(string streamName, bool createIfNecessary)
        }

        public void GetSelectedObject(Genericobject selection, out objectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, out object selectedObject)
        {
            InternalGetSelectedObject(Genericobject selection, out objectTypeEnum objectType, out NameValueMap additionalData, out ComponentOccurrence containingOccurrence, out object selectedObject)
        }

        public bool HasPrivateStorage(string storageName)
        {
            return bool InternalHasPrivateStorage(string storageName)
        }

        public bool HasPrivateStream(string streamName)
        {
            return bool InternalHasPrivateStream(string streamName)
        }

        public void LockSaveSet()
        {
            InternalLockSaveSet()
        }

        public void Migrate()
        {
            InternalMigrate()
        }

        public void PutInternalName(string name, string number, string custom, out string internalName)
        {
            InternalPutInternalName(string name, string number, string custom, out string internalName)
        }

        public void PutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        {
            InternalPutInternalNameAndRevisionId(string internalNameToken, string revisionIdToken, out string internalName, out string revisionId)
        }

        public void Rebuild()
        {
            InternalRebuild()
        }

        public bool Rebuild2(bool acceptErrorsAndContinue)
        {
            return bool InternalRebuild2(bool acceptErrorsAndContinue)
        }

        public void ReleaseReference()
        {
            InternalReleaseReference()
        }

        public void RevertReservedForWriteByMe()
        {
            InternalRevertReservedForWriteByMe()
        }

        public void Save()
        {
            InternalSave()
        }

        public void Save2(bool saveDependents, object documentsToSave)
        {
            InternalSave2(bool saveDependents, object documentsToSave)
        }

        public void SaveAs(string fileName, bool saveCopyAs)
        {
            InternalSaveAs(string fileName, bool saveCopyAs)
        }

        public void SetMissingAddInBehavior(string clientId, InvCommandTypesEnum disabledCommandTypesEnum, object disabledCommands)
        {
            InternalSetMissingAddInBehavior(string clientId, InvCommandTypesEnum disabledCommandTypesEnum, object disabledCommands)
        }

        public void SetThumbnailSaveOption(InvThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        {
            InternalSetThumbnailSaveOption(InvThumbnailSaveOptionEnum saveOption, string imageFullFileName)
        }

        public void Update()
        {
            InternalUpdate()
        }

        public bool Update2(bool acceptErrorsAndContinue)
        {
            return bool InternalUpdate2(bool acceptErrorsAndContinue)
        }

        #endregion
    }
}
