using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.DesignScript.Runtime;
using InventorServices.Persistence;

namespace InventorLibrary.ModulePlacement
{
    [IsVisibleInDynamoLibrary(false)]
    public class ModuleObject
    {
        private IObjectBinder _binder;

        public ModuleObject(int moduleId, int constraintId, IObjectBinder binder)
        {
            _binder = binder;
            _binder.ContextData.Context = new Tuple<int, int>(moduleId, constraintId);
        }
    }
}
