using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InventorServices.Persistence
{
    public class ModuleContextIndexer : IContextManager
    {
        ReferenceManager refManager;

        dynamic IContextManager.BindingContextManager
        {
            get { return refManager; }
            set { refManager = value; }
        }
    }
}
