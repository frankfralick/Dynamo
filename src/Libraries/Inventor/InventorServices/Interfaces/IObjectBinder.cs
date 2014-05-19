using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices.Persistence
{
    public interface IObjectBinder
    {
        ISerializableId<T> GetObjectKey<T>();
        bool GetObjectFromTrace<T>(out T e);

        //TODO: Think about a good way to get rid of this ridiculous parameter.
        void SetObjectForTrace<T>(dynamic objectToBind, Func<List<Tuple<string, int, int, byte[]>>,
                                                         Tuple<string, int, int, byte[]>,
                                                         List<Tuple<string, int, int, byte[]>>> referenceKeysEvaluator);
        IContextData ContextData { get; }
        IContextManager ContextManager { get; }
    }
}
