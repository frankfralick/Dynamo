using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices.Persistence
{
    public class ReferenceKeyBinder2 : IObjectBinder
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";
        private ISerializableIdManager _idManager;
        private ISerializableId<byte[]> _id;
        private IContextData _contextData;
        private IContextManager _contextManager;

        public ReferenceKeyBinder2(ISerializableIdManager idManager, 
                                   ISerializableId<byte[]> id,
                                   IContextData contextData,
                                   IContextManager contextManager)
        {
            _idManager = idManager;
            _id = id;
            _contextData = contextData;
            _contextManager = contextManager;
        }
        public ISerializableId<T> GetObjectKey<T>()
        {
            throw new NotImplementedException();
        }

        public bool GetObjectFromTrace<T>(out T e)
        {
            throw new NotImplementedException();
        }

        public void SetObjectForTrace<T>(dynamic objectToBind)
        {
            throw new NotImplementedException();
        }

        public IContextData ContextData
        {
            get { throw new NotImplementedException(); }
        }

        public IContextManager ContextManager
        {
            get { throw new NotImplementedException(); }
        }
    }
}
