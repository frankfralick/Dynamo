using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices.Persistence
{
    public interface ISerializableIdManager<T>
    {
        T Id { get; set; }
        void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
