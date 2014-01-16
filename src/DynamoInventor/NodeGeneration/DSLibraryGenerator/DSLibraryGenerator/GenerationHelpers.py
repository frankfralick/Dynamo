import clr
clr.AddReference('System')
clr.AddReference('System.Core')
clr.AddReference('System.Reflection')
inventorAss = clr.AddReference('Autodesk.Inventor.Interop')
import System
from System import *
from System.Reflection import *

import Inventor
clr.ImportExtensions(System.Linq)



class ClassGenerator:
    def __init__(self, using_statements, type_from_assembly, target_types, destination_namespace, wrapper_abbreviation, destination_folder):
        self.using_statements = using_statements
        self.type_from_assembly = type_from_assembly
        self.assembly = Assembly.GetAssembly(self.type_from_assembly)
        self.target_types = target_types
        self.destination_namespace = destination_namespace
        self.wrapper_abbreviation = wrapper_abbreviation
        self.destination_folder = destination_folder
        self.wrapper_classes = self.target_types.Select(lambda c: ClassToWrap(self.assembly, c, self.wrapper_abbreviation)).ToList()

        self.generate_classes()


        
        
        #create private mutators

    def generate_classes(self):
        for wrapper in self.wrapper_classes:
            file_path = self.destination_folder + wrapper.file_name
            #argument_name = lambda a: a[:1].lower() + a[1:] if a else ''
            with open(file_path, 'w') as class_file:

                self.write_using_directives(class_file)

                self.write_namespace(class_file)

                self.write_class_declaration(class_file, wrapper) #what about inheritance

                self.write_internal_properties(class_file, wrapper)

                #create private constructors
                self.write_private_constructors(class_file, wrapper)


                #create private mutators
                self.write_private_methods(class_file, wrapper)
                class_file.write('\n')


                #create public properties
                class_file.write(self.tab(2) + '#region Public properties\n')
                class_file.write(self.tab(2) + 'public ' + self.assembly.GetTypes()[0].ToString().split('.')[0] + '.' + 
                         wrapper.target_name + ' ' + wrapper.target_name + 'Instance\n')
                class_file.write(self.tab(2) + '{\n')
                class_file.write(self.tab(3) + 'get { return ' + 'Internal' + wrapper.target_name + '; }\n')
                class_file.write(self.tab(3) + 'set { ' + 'Internal' + wrapper.target_name + ' = value; }\n')
                class_file.write(self.tab(2) + '}\n')
                class_file.write(self.tab(2) + '#endregion\n')
                class_file.write('\n')


                #create public static constructions
                class_file.write(self.tab(2) + '#region Public static constructors\n')
                #probably don't need this at all
                class_file.write(self.tab(2) + 'public static ' + wrapper.name + ' By' + wrapper.name + '(' + wrapper.name + ' ' + self.format_argument_name(wrapper.name) + ')\n')
                class_file.write(self.tab(2) + '{\n')
                class_file.write(self.tab(3) + 'return new ' + wrapper.name + '(' + self.format_argument_name(wrapper.name) + ');\n')
                class_file.write(self.tab(2) + '}\n')
                class_file.write(self.tab(2) + '#endregion\n')
                class_file.write('\n')

                #create public methods
                self.write_public_methods(class_file, wrapper)


                class_file.write(self.tab(1) + '}\n') 
                class_file.write('}\n')
                

    def format_argument_name(self, argument_name):
        formatted_name  = lambda a: a[:1].lower() + a[1:] if a else ''
        return formatted_name(argument_name)

    def get_arguments_string(self, method_arguments):
        #for method in method_arguments:
            #print method[0] + ' ' + method[1]
        return '(' + ', '.join((self.get_type_aliases(method_argument[0], ref_or_out = method_argument[2]) + ' ' + self.format_argument_name(method_argument[1])) for method_argument in method_arguments) + ')\n'

    def get_method_string(self, method_arguments):
        return '(' + ', '.join((self.get_type_aliases(method_argument[0], None, True, ref_or_out = method_argument[2]) + ' ' + self.format_argument_name(method_argument[1])) for method_argument in method_arguments) + ');\n'

    #def get_method_string(self, method_arguments):
        #return '(' + ', '.join((self.format_argument_name(method_argument[1])) for method_argument in method_arguments) + ');\n'

    def get_read_only_property_text(self, access_modifier, method_info):
        if access_modifier == 'internal':
            property_text = (self.tab(2) + 
                             access_modifier + ' ' + 
                             self.get_type_aliases(method_info.return_type.Name, access_modifier) + ' ' + 
                             'Internal' + method_info.c_sharp_name + ' ' + '{ get; }\n')
        else:
            property_text = (self.tab(2) + 
                             access_modifier + ' ' + 
                             self.get_type_aliases(method_info.return_type.Name) + ' ' + 
                             method_info.c_sharp_name + ' ' + '{ get; }\n')
        return property_text   

    def get_read_write_property_text(self, access_modifier, method_info):
        if access_modifier == 'internal':
            property_text = (self.tab(2) + 
                             access_modifier + ' ' + 
                             self.get_type_aliases(method_info.return_type.Name, access_modifier) + ' ' + 
                             'Internal' + method_info.c_sharp_name + ' ' + '{ get; set; }\n')
        else:
            property_text = (self.tab(2) + 
                             access_modifier + ' ' + 
                             self.get_type_aliases(method_info.return_type.Name) + ' ' + 
                             method_info.c_sharp_name + ' ' + '{ get; set; }\n')
        return property_text

    def get_type_aliases(self, possible_system_type, access_modifier = None, method_body = False, ref_or_out = ''):
        built_in_alias_table = [('Boolean', 'bool'),
                                ('Byte', 'byte'),
                                ('Char', 'char'),
                                ('Decimal', 'decimal'),
                                ('Double', 'double'),
                                ('Int16', 'short'),
                                ('Int32', 'int'),
                                ('Int64', 'long'),
                                #('Object', 'object'), #need to fix this ie object[] vs ObjectVisibility
                                ('SByte', 'sbyte'), 
                                ('Single', 'float'),
                                ('String', 'string'),
                                ('UInt16', 'ushort'),
                                ('UInt32', 'uint'),
                                ('UInt64', 'ulong'),
                                ('Void', 'void')]

        #it may be an out system type parameter
        if method_body == False:
            if possible_system_type[-1] == '&':
                if built_in_alias_table.Any(lambda t: possible_system_type.Contains(t[0])):
                    return possible_system_type.Replace(built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[0], 
                                                        ref_or_out + built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[1])[:-1]
                else:
                    return ref_or_out + possible_system_type[:-1]
            #otherwise this is an out parameter from some other namespace without alias
            elif built_in_alias_table.Any(lambda t: possible_system_type.Contains(t[0])):
                return possible_system_type.Replace(built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[0], 
                                                    built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[1])
            else:
                return possible_system_type
        elif method_body == True:
            if possible_system_type[-1] == '&':
                return ref_or_out
            else:
                return ''

        elif built_in_alias_table.Any(lambda t: possible_system_type.Contains(t[0])):
            return possible_system_type.Replace(built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[0], 
                                                built_in_alias_table.First(lambda t: possible_system_type.Contains(t[0]))[1])
        else:
            if access_modifier == None:
                return self.wrapper_abbreviation + possible_system_type
            else:
                return possible_system_type
        
    def get_wrapper_name(self, type_name):
        return self.wrapper_abbreviation + type_name

    def tab(self, quantity):
        return ' '*4*quantity

    def write_class_declaration(self, class_file, wrapper):
        class_file.write(self.tab(1) + '[RegisterForTrace]\n')
        class_file.write(self.tab(1) + 'public class ' + wrapper.name + '\n')
        class_file.write(self.tab(1) + '{\n')

    def write_internal_properties(self, class_file, wrapper):
        class_file.write(self.tab(2) + '#region Internal properties\n')
        #create internal property to hold the instance being wrapped
        class_file.write(self.tab(2) + 'internal ' + self.assembly.GetTypes()[0].ToString().split('.')[0] + '.' + 
                         wrapper.target_name + ' ' + 'Internal' + wrapper.target_name + ' ' + 
                         '{ get; set; }\n')
        class_file.write('\n')
        #create the read only properties
        for i in range(len(wrapper.members.read_only_properties)):
            class_file.write(self.get_read_only_property_text('internal',
                                                              wrapper.members.read_only_properties[i]))
            class_file.write('\n')
        #create the read write properties
        for i in range(len(wrapper.members.read_write_properties)-1):
            class_file.write(self.get_read_write_property_text('internal', 
                                                               wrapper.members.read_write_properties[i]))

            class_file.write('\n')

        if len(wrapper.members.read_write_properties) > 0:
            class_file.write(self.get_read_write_property_text('internal', wrapper.members.read_write_properties[-1]))
            class_file.write(self.tab(2) + '#endregion\n')
        class_file.write('\n')

    def write_method_declaration(self, class_file, method, method_access_modifier):
        if method_access_modifier == 'private':
            class_file.write(self.tab(2) + method_access_modifier + ' ' + 
                                self.get_type_aliases(method.return_type.Name) + ' Internal' + 
                                method.c_sharp_name + 
                                self.get_arguments_string(method.arguments))
        else:
            class_file.write(self.tab(2) + method_access_modifier + ' ' + 
                        self.get_type_aliases(method.return_type.Name) + ' ' + 
                        method.c_sharp_name + 
                        self.get_arguments_string(method.arguments))

    def write_private_constructors(self, class_file, wrapper):
        class_file.write(self.tab(2) + '#region Private constructors\n')     
        class_file.write(self.tab(2) + 'private ' +
                         wrapper.name + '(' + 
                         wrapper.name + ' ' + 
                         self.format_argument_name(wrapper.name) + ')\n')
        class_file.write(self.tab(2) + '{\n')
        class_file.write(self.tab(3) + 'Internal' + 
                         wrapper.target_name + ' = ' + 
                         self.format_argument_name(wrapper.name) + '.Internal' + wrapper.target_name + ';\n')
        class_file.write(self.tab(2) + '}\n')      
        class_file.write(self.tab(2) + '#endregion\n')
        class_file.write('\n')

    def write_private_methods(self, class_file, wrapper):
        class_file.write(self.tab(2) + '#region Private methods\n')
        method_access_modifier = 'private'
        for method in wrapper.members.methods:
            if method.c_sharp_name[0] != '_':
                self.write_method_declaration(class_file, method, method_access_modifier)
                class_file.write(self.tab(2) + '{\n')

                #if return type is void, just call the internal method
                if self.get_type_aliases(method.return_type.Name) == 'void':
                    #class_file.write(self.tab(3) + self.assembly.GetTypes()[0].ToString().split('.')[0] + '.' + method.c_sharp_name + self.get_method_string(method.arguments))
                    class_file.write(self.tab(3)  + wrapper.target_name + 'Instance' + '.' + method.c_sharp_name + self.get_method_string(method.arguments))
                else:
                    #class_file.write(self.tab(3) + 'return '+ self.get_type_aliases(method.return_type.Name, None, True) + ' ' +' Internal' + method.c_sharp_name + self.get_method_string(method.arguments))
                    class_file.write(self.tab(3) + 'return ' + wrapper.target_name + 'Instance' + '.' + method.c_sharp_name + self.get_method_string(method.arguments))
                class_file.write(self.tab(2) + '}\n')
                class_file.write('\n')
        class_file.write(self.tab(2) + '#endregion\n')



    def write_public_methods(self, class_file, wrapper):
        class_file.write(self.tab(2) + '#region Public methods\n')
        method_access_modifier = 'public'
        for method in wrapper.members.methods:
            if method.c_sharp_name[0] != '_':
                self.write_method_declaration(class_file, method, method_access_modifier)
                class_file.write(self.tab(2) + '{\n')
                #if return type is void, just call the internal method
                if self.get_type_aliases(method.return_type.Name) == 'void':
                    class_file.write(self.tab(3) + 'Internal' + method.c_sharp_name + self.get_method_string(method.arguments))
                #if there is a return type, return the result of calling the internal method
                else:
                    class_file.write(self.tab(3) + 'return Internal' + method.c_sharp_name + self.get_method_string(method.arguments))
                class_file.write(self.tab(2) + '}\n')
                class_file.write('\n')
        class_file.write(self.tab(2) + '#endregion\n')

    def write_namespace(self, class_file):
        class_file.write('namespace ' + self.destination_namespace + '\n')
        class_file.write('{\n')

    def write_using_directives(self, class_file):
        class_file.writelines(self.using_statements.Select(lambda u: 'using ' + u + ';\n'))
        class_file.write('\n')


class ClassToWrap:
    def __init__(self, assembly, target_type_name, wrapper_abbreviation):
        self.assembly = assembly
        self.target_type = self.assembly.GetType(target_type_name)
        self.members = WrappedClassMembers(self.target_type.GetMethods().ToList()
                                           .Where(lambda y: y.IsPublic)
                                           .OrderBy(lambda p: p.Name))
        self.target_name = self.target_type.Name
        self.name = wrapper_abbreviation + self.target_type.Name
        self.file_name = self.name + '.cs'
               
class WrappedClassMembers:
    def __init__(self, member_info):
        self.all_members = [Method(m) for m in member_info]
        self.read_write_properties = [Method(m) for m in member_info
                                      .Where(lambda m: m.Name[:4] == 'get_')
                                      .Where(lambda p: member_info.Any(lambda k: (k.Name[4:] == p.Name[4:]) & (k.Name[:4] == 'set_')))
                                      .Where(lambda a: a.GetParameters().Count == 0).ToList()]

        self.read_only_properties = []

        get_members = self.all_members.Where(lambda p: p.name[:4] == 'get_')
        set_members = self.all_members.Where(lambda p: p.name[:4] == 'set_')
        for get_member in get_members:
            if set_members.Any(lambda k: k.name[4:] == get_member.name[4:]):
                pass
            else:
                self.read_only_properties.append(get_member)
                
        for prop in self.read_only_properties:
            print prop.name
        #this one in particular is suspect. 
        self.methods = [Method(m) for m in member_info
                        .Where(lambda p: self.read_write_properties.Any(lambda q: q.name != p.Name))
                        .Where(lambda p: self.read_only_properties.Any(lambda q: q.name != p.Name))
                        .Where(lambda p: p.Name[:4] != 'set_')
                        .Where(lambda p: p.Name[:4] != 'get_')]

        print "All members:  " + str(self.all_members.Count)
        print "Read-only:   " + str(self.read_only_properties.Count)
        print "Read-write:   " + str(self.read_write_properties.Count)
        print "Methods:    " + str(self.methods.Count)
   
class Method:
    def __init__(self, method_info):
        self.method_info = method_info
        self.name = self.method_info.Name
        if (self.name[:4] == 'get_') | (self.name[:4] == 'set_'):
            self.c_sharp_name = self.name[4:]
        else:
            self.c_sharp_name = self.name
        self.return_type = self.method_info.ReturnType
        self.arguments = self.method_info.GetParameters().Select(lambda p: [p.ParameterType.Name, p.Name, self.get_is_byref_or_out(p)])

    def get_is_byref_or_out(self, parameter):
        if parameter.IsOut:
            if parameter.IsIn:      
                return 'ref '
            else:
                return 'out '
        else:
            return ''

    def get_is_out(self, parameter):
        if parameter.IsOut:
            #print "parameter " + parameter.Name + ", of type '" + parameter.ParameterType.Name + "' is out."
            return 'out '
        else:
            return ''

StringComparer = System.Collections.Generic.IEqualityComparer[type(Method)]
class NameComparer(StringComparer):
    def get_Equals(self, method_object):
        if self.name == method_object.name:
            return True
        else:
            return False 

#IronPython documentation is miz.  So many things possible with no documentation.
#class MethodE (IEquatable[type(Method)], Method):
#    def __init__(self, method_info):
#        Method.__init__(self, method_info)
    
#    def Equals(self, method_object):
#        if self.name == method_object.name:
#            return True
#        else:
#            return False  

#class Method:
#    def __init__(self, method_info):
#        self.method_info = method_info
#        self.name = self.method_info.Name
#        if (self.name[:4] == 'get_') | (self.name[:4] == 'set_'):
#            self.c_sharp_name = self.name[4:]
#        else:
#            self.c_sharp_name = self.name
#        self.return_type = self.method_info.ReturnType
#        self.arguments = self.method_info.GetParameters().Select(lambda p: [p.ParameterType.Name, p.Name])

#class MethodE (IEquatable[type(Method)], Method):
#    def __init__(self, method_info):
#        Method.__init__(self, method_info)
    
#    def Equals(self, method_object):
#        if self.name == method_object.name:
#            return True
#        else:
#            return False  