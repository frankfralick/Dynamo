using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Inventor;
using DSNodeServices;


namespace InventorServices.Persistence
{

    [Serializable]
    public class SerializableId : ISerializable
    {
        public byte[] ReferenceKey { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("referenceKey", ReferenceKey, typeof(byte[]));
        }

        public SerializableId()
        {
            ReferenceKey = new byte[] { };
        }

        /// <summary>
        /// Ctor used by the serialisation engine
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public SerializableId(SerializationInfo info, StreamingContext context)
        {
            ReferenceKey = (byte[])info.GetValue("referenceKey", typeof(byte[]));
        }
    }

    /// <summary>
    /// Class for handling ReferenceKey byte[] in a typesafe manner.
    /// </summary>
    public class ObjectReferenceKey
    {
        public byte[] ReferenceKey { get; set; }

        public ObjectReferenceKey()
        {
            ReferenceKey = new byte[] { }; ;
        }

        public ObjectReferenceKey(byte[] referenceKey)
        {
            this.ReferenceKey = referenceKey;
        }

    }

    public class ReferenceKeyBinder
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";

        public static ObjectReferenceKey GetReferenceKeyFromTrace<T>()  
        {
            ISerializable traceData = TraceUtils.GetTraceData(INVENTOR_TRACE_ID);

            SerializableId id = traceData as SerializableId;
            if (id == null)
                return null;

            var traceDataRefKey = id.ReferenceKey;
            return new ObjectReferenceKey(traceDataRefKey);
        }

        public static bool GetObjectFromTrace<T>(out T e)
        {
            if (GetReferenceKeyFromTrace<T>() != null && 
                TryBindReferenceKey<T>(GetReferenceKeyFromTrace<T>().ReferenceKey, out e))
            {
                return true;
            }

            else
            {
                e = default(T);
                return false;
            }         
        }

        public static void SetObjectForTrace(dynamic inventorObject)
        {
            SerializableId id = new SerializableId();
            byte[] refKey = new byte[] { };
            if (ReferenceManager.KeyManager == null)
            {
                ReferenceManager.KeyManager = PersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
            }
            ReferenceManager.KeyContext = PersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
            inventorObject.GetReferenceKey(ref refKey, (int)ReferenceManager.KeyContext);
            //string stringKey = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.KeyToString(refKey);
            id.ReferenceKey = refKey;
            
            TraceUtils.SetTraceData(INVENTOR_TRACE_ID, id);
        }

        public static bool TryBindReferenceKey<T>(byte[] key, out T e)
        {
            if (ReferenceManager.KeyManager == null)
            {
                PersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)PersistenceManager.InventorApplication.ActiveDocument;
                ReferenceManager.KeyManager = PersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            try
            {
                object outType = null;
                int keyContext;
                byte[] keyContextArray = new byte[] { };
                //TODO: This will not work with BRep objects.  Inventor doesn't care about the KeyContext for anything else.
                //KeyContext is a long.  May be good to have a different set of methods for BRep objects to avoid storing this 
                //additional information when it isn't needed.
                keyContext = PersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();

                ReferenceManager.KeyContext = keyContext;

                //T invObject = (T)ReferenceManager.KeyManager.BindKeyToObject(ref key, (int)ReferenceManager.KeyContext, out outType);
                T invObject = (T)ReferenceManager.KeyManager.BindKeyToObject(ref key, 0, out outType);
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
