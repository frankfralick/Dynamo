using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    public class ModuleContextArray : IContextData
    {
        Tuple<int, int> _contextTuple;
        public ModuleContextArray()
        {
            //_contextTuple = new Tuple<int, int>(0,0);
        }

        public Tuple<int, int> Context
        {
            get
            {
                return _contextTuple;
            }
            set
            {
                _contextTuple = value;
            }
        }
    }
}
