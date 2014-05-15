using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices.Persistence
{
    [Serializable]
    public class ModuleIdManager : ISerializableIdManager<List<Tuple<string, int, int, byte[]>>>, 
                                   ISerializable
    {
        private List<Tuple<string, int, int, byte[]>> _id;

        public List<Tuple<string, int, int, byte[]>> Id 
        {
            get 
            {
                if (_id != null)
                {
                    return _id;
                }
                else
                {
                    return null;
                }
                 
            }
            set { _id = value; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("referenceKey", Id, Id.GetType());
        }

        public ModuleIdManager()
        {
            _id = new List<Tuple<string, int, int, byte[]>>();
        }

        public ModuleIdManager(SerializationInfo info, StreamingContext context)
        {
            Id = (List<Tuple<string, int, int, byte[]>>)info.GetValue("referenceKey", Id.GetType());
        }
    }
}
