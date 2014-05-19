using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    public interface IBindableObject
    {
        IObjectBinder Binder { get; set; }
        dynamic ObjectToBind { get; set; }
        void RegisterContextData(int moduleId, int objectId);
    }
}
