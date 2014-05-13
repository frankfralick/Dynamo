using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    interface IObjectBinder
    {
        IObjectKey<T> GetObjectKey<T>();
        bool GetObjectFromTrace<T>(IContextArray contextArray, IContextManager contextManager, out T e);
        void SetObjectForTrace<T>();

    }
}
