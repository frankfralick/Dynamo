using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Dynamo;
using Dynamo.FSchemeInterop;
using Dynamo.Interfaces;
using Dynamo.UpdateManager;
using InventorServices;
using InventorServices.Persistence;
using InventorLibrary.ModulePlacement;

namespace Dynamo
{
    public class DynamoController_Inventor : DynamoController
    {
        public DynamoController_Inventor(string context, IUpdateManager updateManager)
            : base(
                context,
                updateManager,
                new DefaultWatchHandler(),
                Dynamo.PreferenceSettings.Load())
        {
            EngineController.ImportLibrary("InventorLibrary.dll");

            //Create and configure IoC container for InventorServices
            PersistenceManager.LetThereBeIoC();

            //Create IoC container for the ModulePlacement portion of the library.
            ModuleIoC.LetThereBeIoC();

            //The compiler can't know about any registration/dependency graph errors.  The container's Verify method 
            //lets SimpleInjector build all of these registrations so the application will fail at startup if we have 
            //made a mistake.
            PersistenceManager.IoC.Verify();

            var testUser = PersistenceManager.IoC.GetInstance<InventorServices.Persistence.ITestInterface>();

        }

        protected override void Evaluate()
        {
            base.Evaluate();
        }
    }
}
