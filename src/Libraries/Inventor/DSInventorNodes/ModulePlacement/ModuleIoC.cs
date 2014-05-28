using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventorServices.Persistence;
using SimpleInjector;
using Autodesk.DesignScript.Runtime;

namespace InventorLibrary.ModulePlacement
{
    [IsVisibleInDynamoLibrary(false)]
    public class ModuleIoC
    {
        //public static Container IoC { get; set; }

        public static void LetThereBeIoC()
        {
            //Container container = PersistenceManager.IoC;
            //Container container = new Container();
            //IoC = container;
            PersistenceManager.IoC.Register<IModules, Modules>(Lifestyle.Singleton);
            PersistenceManager.IoC.Register<IPointsList, ModulePoints>(Lifestyle.Singleton);


            //The compiler can't know about any registration/dependency graph errors.  The container's Verify method 
            //lets SimpleInjector build all of these registrations so the application will fail at startup if we have 
            //made a mistake.
            //PersistenceManager.IoC.Verify();
        }
    }
}
