import GenerationHelpers as gh
import Inventor


#define list of namespaces these clases will be using
using = ['System',
         'System.Collections.Generic',
         'System.ComponentModel',
         'System.Linq',
         'System.Text',
         'Inventor',
         'Autodesk.DesignScript.Geometry',
         'Autodesk.DesignScript.Interfaces',
         'DSNodeServices',
         'Dynamo.Models',
         'Dynamo.Utilities',
         'DSInventorNodes.GeometryConversion',
         'InventorServices.Persistence']

#define a type in the assembly these classes will be generated from.
type_from_assembly = Inventor.Application

#'Inventor.AssemblyDocument'
#'Inventor.PartDocument',
#'Inventor.CommandIDEnum',
#'Inventor._DocPerformanceMonitor',
#'Inventor.Assets',
#'Inventor.AttributeManager',
#'Inventor.AttributeManager',
#'Inventor.AttributeSets',
#'Inventor.BrowserPanel',
#'Inventor.CachedGraphicsStatus',
#'Inventor.AssemblyComponentDefinition',
#'Inventor.AssemblyComponentDefinitions',
#'Inventor.DisabledCommandList',
#'Inventor.DisplaySettings',
#'Inventor.DocumentEvents',
#'Inventor.DocumentInterests',
#'Inventor.DocumentSubType',
#'Inventor.DocumentTypeEnum',
#'Inventor.EnvironmentManager',
#'Inventor.File',
#'Inventor.GraphicDataSetsCollection',
#'Inventor.FileOwnershipEnum',
#'Inventor.PrintManager',
#'Inventor.PropertySets',
#'Inventor.CommandTypesEnum',
#'Inventor.DocumentDescriptorsEnumerator',
#'Inventor.ReferencedFileDescriptors',
#'Inventor.ReferencedOLEFileDescriptors',
#'Inventor.ReferenceKeyManager',
#'Inventor.RenderStyles',
#'Inventor.SelectSet',
#'Inventor.SketchSettings',
#'Inventor.SoftwareVersion',
#'Inventor.ThumbnailSaveOptionEnum',
#'Inventor.OGSSceneNode',
#'Inventor.ObjectTypeEnum',
#'Inventor.UnitsOfMeasure',
#'Inventor.InventorVBAProject',
#'Inventor.Views',
#'Inventor.LightingStyle',
#'Inventor.CommandTypesEnum',
#'Inventor.SelectionPriorityEnum'


#if generating multiple classes at once, limit to types in the same namespace.
types_to_generate = ['Inventor.Documents']

#define the namespace the generated classes will be part of.
destination_namespace = 'DSInventorNodes'

#define an prefix for the wrapper type name:
prefix = "Inv"

#define the folder to save class files to:
destination_folder = "C:\\Projects\\Dynamo\\Dynamo\\scripts\\NodeGenerator\\Tests\\"
#destination_folder = "C:\\Projects\\Dynamo\\Dynamo\\src\\DSInventorNodes\\API\\"

generator = gh.ClassGenerator(using, type_from_assembly, types_to_generate, destination_namespace, prefix, destination_folder)



