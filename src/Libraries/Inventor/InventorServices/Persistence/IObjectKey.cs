using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    interface IObjectKey<T>
    {
        T ObjectKey { get; set; }
    }
}
