using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using SimpleInjector;
using SimpleInjector.Extensions;

using Inventor;



namespace InventorServices.Persistence
{
    /// <summary>
    /// This class holds static references that the application needs.  
    /// </summary>
    public class PersistenceManager
    {   //TODO Probably git rid of all this static junk.  It doesn't seem like a good idea.
        //TODO Implement IDisposable
        private static ApprenticeServerComponentClass apprenticeServer;

        public static AssemblyDocument ActiveAssemblyDoc { get; set; }

        public static PartDocument ActivePartDoc { get; set; }

        public static Document ActiveDocument
        {
            get { return InventorApplication.ActiveDocument; }
        }
        
        public static Inventor.Application InventorApplication { get; set; }

        public static ApprenticeServerComponent ActiveApprenticeServer
        {
            get
            {
                if (apprenticeServer == null)
                {
                    apprenticeServer = new ApprenticeServerComponentClass();
                }
                return apprenticeServer;
            }

            set { value = apprenticeServer; }
        }
                
        //This is the name of the storage for Dynamo in Inventor files.
        //Not currently using this.
        private static string dynamoStorageName = "Dynamo";

        public static string DynamoStorageName
        {
            get { return dynamoStorageName; }
        }

        public static Container IoC { get; set; }

        public static void LetThereBeIoC()
        {
            Container container = new Container();
            IoC = container;
            IoC.Register<ITestInterface, TestImplementation>(Lifestyle.Transient);
            IoC.Register<IModuleBinder, ModuleReferenceKeyBinder>(Lifestyle.Transient);
            IoC.Register<IObjectBinder, ReferenceKeyBinder>(Lifestyle.Transient);
            IoC.Register<IBindableObject, ModuleObject>(Lifestyle.Transient);
            IoC.Register<IContextData, ModuleContextArray>(Lifestyle.Transient);
            IoC.Register<ISerializableModuleIdManager, ModuleIdManager>(Lifestyle.Transient);
            IoC.Register<ISerializableIdManager, ObjectIdManager>(Lifestyle.Transient);

            //Batch register is not possible for ISerializableId<T> for all T because it must
            //have more than one constructor.
            IoC.Register<ISerializableId<List<Tuple<string, int, int, byte[]>>>>(() => new ModuleId());
            IoC.Register<ISerializableId<byte[]>>(() => new ObjectId());
            IoC.Register<IContextManager, ModuleContextManager>(Lifestyle.Transient);
        }
    }
}
