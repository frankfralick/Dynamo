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

    public class ReferenceKeyBinder
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";

        public static byte[] GetReferenceKeyFromTrace<T>()
        {
            //Get the element ID that was cached in the callsite
            ISerializable traceData = TraceUtils.GetTraceData(INVENTOR_TRACE_ID);
            if (ReferenceManager.KeyManager == null)
            {
                ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            SerializableId id = traceData as SerializableId;
            if (id == null)
                return null; 

            return id.ReferenceKey;
        }

        public static bool GetObjectFromTrace<T>(out T e)
        {
            byte[] refKey = GetReferenceKeyFromTrace<T>();

            if (refKey != null && TryBindReferenceKey<T>(refKey, out e))
                return true;
            else
                e = default(T);
                return false;
        }

        public static void SetObjectForTrace(dynamic inventorObject)
        {
            SerializableId id = new SerializableId();
            byte[] refKey = new byte[] { };
            id.ReferenceKey = inventorObject.GetReferenceKey(ref refKey, (int)ReferenceManager.KeyContext);
            TraceUtils.SetTraceData(INVENTOR_TRACE_ID, id);
        }

        public static bool TryBindReferenceKey<T>(byte[] key, out T e)
        {
            if (ReferenceManager.KeyManager == null)
            {
                InventorPersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)InventorPersistenceManager.InventorApplication.ActiveDocument;
                ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            try
            {
                object outType = null;
                int keyContext;
                byte[] keyContextArray = new byte[] { };
                keyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();

                ReferenceManager.KeyContext = keyContext;

                T invObject = (T)ReferenceManager.KeyManager.BindKeyToObject(ref key, (int)ReferenceManager.KeyContext, out outType);
                e = invObject;
                return invObject != null;
            }
            catch
            {
                //Can't set e to null because it might not be nullable, using default(T) instead.
                e = default(T);
                return false;
            }
        }
    }
}
