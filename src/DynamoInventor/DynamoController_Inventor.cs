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
        }

        protected override void Evaluate()
        {
            base.Evaluate();
        }
    }
}
