using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    public class ModuleReferenceKeyBinder : IObjectBinder
    {
        private ISerializableIdManager<List<Tuple<string, int, int, byte[]>>> _idManager;
        IContextArray _contextArray;
        IContextManager _contextManager;

        public ModuleReferenceKeyBinder(ISerializableIdManager<List<Tuple<string, int, int, byte[]>>> idManager, 
                                        IContextArray contextArray, 
                                        IContextManager contextManager)
        {
            _idManager = idManager;
            _contextArray = contextArray;
            _contextManager = contextManager;
        }

        public IObjectKey<T> GetObjectKey<T>()
        {
            throw new NotImplementedException();
        }

        public bool GetObjectFromTrace<T>(out T e)
        {
            
            var id = _idManager.Id;
            if (id != null)
            {
                var matchedData = id.Where(p => p.Item1 == typeof(T).ToString())
                                                                      .Where(q => q.Item2 == _contextArray.Context.Item1)
                                                                      .Where(r => r.Item3 == _contextArray.Context.Item2)
                                                                      .FirstOrDefault();
                if (matchedData != null)
                {
                    e = default(T);
                    return true;
                }
                else
                {
                    e = default(T);
                    return false;
                }
            }
            else
            {
                e = default(T);
                return false;
            }
        }

        public void SetObjectForTrace<T>()
        {
            throw new NotImplementedException();
        }
    }
}
