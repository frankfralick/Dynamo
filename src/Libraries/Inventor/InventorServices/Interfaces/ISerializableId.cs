using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices
{
    interface ISerializableId<T>
    {
        T Key { get; set; }
    }
}
