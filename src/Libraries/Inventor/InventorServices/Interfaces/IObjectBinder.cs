using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    public interface IObjectBinder
    {
        IObjectKey<T> GetObjectKey<T>();
        bool GetObjectFromTrace<T>(out T e);
        void SetObjectForTrace<T>();
    }
}
