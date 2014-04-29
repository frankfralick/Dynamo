using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Dynamo;
using Dynamo.Interfaces;
using InventorServices;

namespace Dynamo
{
    public class DynamoController_Inventor : DynamoController
    {
        public DynamoController_Inventor(Type viewModelType, string context)
            : base(
                context,
                new UpdateManager.UpdateManager(),
                new DefaultWatchHandler(),
                Dynamo.PreferenceSettings.Load())
        {
            EngineController.ImportLibrary("DSInventorNodes.dll");
        }

        protected override void Run()
        {
            base.Run();
        }
    }
}
