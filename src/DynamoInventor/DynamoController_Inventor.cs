using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Dynamo;
using Dynamo.FSchemeInterop;

namespace Dynamo
{
    class DynamoController_Inventor : DynamoController
    {
        public DynamoController_Inventor(FSchemeInterop.ExecutionEnvironment env, Type viewModelType, string context)
            : base(env, viewModelType, context)
        {

        }
    }
}
