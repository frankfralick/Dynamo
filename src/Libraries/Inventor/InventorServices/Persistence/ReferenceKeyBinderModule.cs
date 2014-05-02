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
    public class SerializableModuleId : ISerializable
    {
        public List<byte[]> ReferenceKeys { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("referenceKey", ReferenceKeys, typeof(List<byte[]>));
        }

        public SerializableModuleId()
        {
            ReferenceKeys = new List<byte[]> { };
        }

        /// <summary>
        /// Ctor used by the serialisation engine
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public SerializableModuleId(SerializationInfo info, StreamingContext context)
        {
            ReferenceKeys = (List<byte[]>)info.GetValue("referenceKey", typeof(List<byte[]>));
        }
    }

    /// <summary>
    /// Class for handling ReferenceKey byte[] in a typesafe manner.
    /// </summary>
    public class ModuleReferenceKeys
    {
        public List<byte[]> ReferenceKeys { get; set; }

        public ModuleReferenceKeys()
        {
            ReferenceKeys = new List<byte[]> { }; ;
        }

        public ModuleReferenceKeys(List<byte[]> referenceKey)
        {
            this.ReferenceKeys = referenceKey;
        }

    }

    public class ReferenceKeyBinderModule
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";

        public static ModuleReferenceKeys GetReferenceKeyFromTrace<T>()  
        {
            ISerializable traceData = TraceUtils.GetTraceData(INVENTOR_TRACE_ID);

            SerializableModuleId id = traceData as SerializableModuleId;
            if (id == null)
                return null;

            var traceDataRefKey = id.ReferenceKeys;
            return new ModuleReferenceKeys(traceDataRefKey);
        }

        public static bool GetObjectFromTrace<T>(out T e)
        {
            //if (GetReferenceKeyFromTrace<T>() != null)// && TryBindReferenceKey<T>(GetReferenceKeyFromTrace<T>().ReferenceKeys, out e))
            if (GetReferenceKeyFromTrace<T>() != null)
            {
                List<byte[]> refKeys = GetReferenceKeyFromTrace<T>().ReferenceKeys;
                foreach (var refKey in refKeys)
                {
                    TryBindReferenceKey<T>(refKey, out e);
                    if (e != null)
                    {
                        return true;
                    }
                }
                e = default(T);
                return false;
            }

            else
            {
                e = default(T);
                return false;
            }         
        }

        public static void SetObjectForTrace<T>(dynamic inventorObject)
        {
            byte[] refKey = new byte[] { };

            //SetObjectForTrace has been called and we need to check if there is anything it the slot.
            List<byte[]> refKeys = GetReferenceKeyFromTrace<T>().ReferenceKeys;
            if (refKeys != null)
            {
                if (ReferenceManager.KeyManager == null)
                {
                    ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
                }
                ReferenceManager.KeyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                inventorObject.GetReferenceKey(ref refKey, (int)ReferenceManager.KeyContext);
                //Since we got a list back, and we already know that binding attempts failed, we can just add this now key to the end.
                refKeys.Add(refKey);
            }

            else
	        {
                SerializableModuleId id = new SerializableModuleId();
                
                if (ReferenceManager.KeyManager == null)
                {
                    ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
                }
                ReferenceManager.KeyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                inventorObject.GetReferenceKey(ref refKey, (int)ReferenceManager.KeyContext);
                id.ReferenceKeys = new List<byte[]>{refKey};
                TraceUtils.SetTraceData(INVENTOR_TRACE_ID, id);
	        }
             
            
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
                //TODO: This will not work with BRep objects.  Inventor doesn't care about the KeyContext for anything else.
                //KeyContext is a long.  May be good to have a different set of methods for BRep objects to avoid storing this 
                //additional information when it isn't needed.
                keyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();

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
