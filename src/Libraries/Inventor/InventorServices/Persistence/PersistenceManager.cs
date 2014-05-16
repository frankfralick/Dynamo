using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using SimpleInjector;

using Inventor;


namespace InventorServices.Persistence
{
    /// <summary>
    /// This class holds static references that the application needs.  
    /// </summary>
    public class PersistenceManager
    {
        //public InventorPersistenceManager()
        //{
        //}
        //TODO Dispose of this!
        private static ApprenticeServerComponentClass apprenticeServer;

        public static AssemblyDocument ActiveAssemblyDoc { get; set; }

        public static PartDocument ActivePartDoc { get; set; }

        public static Document ActiveDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
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
                
        //This is the name of the storage for Dynamo object bindings.
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
            IoC.Register<IObjectBinder, ModuleReferenceKeyBinder>(Lifestyle.Transient);
            IoC.Register<IContextData, ModuleContextArray>(Lifestyle.Transient);
            //Implementations of ISerializableIdManager need a second contstructor for the serialization engine to call,
            //so this registration needs to have a delegate to the default constructor so SimpleInjector knows what to do.
            IoC.Register<ISerializableIdManager<List<Tuple<string, int, int, byte[]>>>>(() => new ModuleIdManager());
            IoC.Register<IContextManager, ModuleContextManager>();
        }
    }
}
