using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InventorServices.Persistence
{
    public class ReferenceKeyBinder : IObjectBinder
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";
        private ISerializableIdManager _idManager;
        private ISerializableId<byte[]> _id;
        private IContextData _contextData;
        private IContextManager _contextManager;

        public ReferenceKeyBinder(ISerializableIdManager idManager, 
                                   ISerializableId<byte[]> id,
                                   IContextData contextData,
                                   IContextManager contextManager)
        {
            _idManager = idManager;
            _id = id;
            _contextData = contextData;
            _contextManager = contextManager;
        }

        public IContextData ContextData
        {
            get { return _contextData; }
        }

        public IContextManager ContextManager
        {
            get { return _contextManager; }
        }

        public ISerializableId<T> GetObjectKey<T>()
        {
            throw new NotImplementedException();
        }

        public bool GetObjectFromTrace<T>(out T e)
        {
            ISerializableId<byte[]> refKey;
            if (_idManager.GetTraceData(INVENTOR_TRACE_ID, out refKey) && refKey != null && TryBindReferenceKey<T>(refKey.Id, out e))
            {
                return true;
            }
            else
            {
                e = default(T);
                return false;
            }
        }

        public void SetObjectForTrace<T>(dynamic objectToBind)
        {
            byte[] refKey = new byte[] { };
            objectToBind.GetReferenceKey(ref refKey, 0);
            _id.Id = refKey;
            _idManager.SetTraceData(INVENTOR_TRACE_ID, _id as ISerializable);    
        }

        public bool TryBindReferenceKey<T>(byte[] key, out T e)
        {
            try
            {
                object outType = null;
                byte[] keyContextArray = new byte[] { };

                //TODO: This will not work with BRep objects.  Inventor doesn't care about the KeyContext for anything else.
                //KeyContext is a long.  May be good to have a different set of methods for BRep objects to avoid storing this 
                //additional information when it isn't needed.
                //
                T invObject = (T)_contextManager.BindingContextManager.BindKeyToObject(ref key, 0, out outType);
                e = invObject;
                return invObject != null;
            }

            catch
            {
                e = default(T);
                return false;
            }
        }
    }
}
