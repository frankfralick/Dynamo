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

#define a list of types to generate nodes
types_to_generate = ['Inventor.Documents',
                     'Inventor.AssemblyDocument',
                     'Inventor.CameraEventsClass']

#define the namespace the generated classes will be part of.
destination_namespace = 'DSInventorNodes'

#define an prefix for the wrapper type name:
prefix = "Inv"

#define the folder to save class files to:
destination_folder = "C:\\Projects\\Dynamo\\Dynamo\\scripts\\NodeGenerator\\Tests\\"

generator = gh.ClassGenerator(using, type_from_assembly, types_to_generate, destination_namespace, prefix, destination_folder)



