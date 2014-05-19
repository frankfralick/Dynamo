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
        //Each Tuple represents: (string Type, int ModuleNumber, int ConstraintIndex, byte[] ReferenceKey)
        public List<Tuple<string, int, int, byte[]>> ReferenceKeys { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("referenceKey", ReferenceKeys, typeof(List<Tuple<string, int, int, byte[]>>));
        }

        public SerializableModuleId()
        {
            ReferenceKeys = new List<Tuple<string, int, int, byte[]>> { };
        }

        /// <summary>
        /// Ctor used by the serialisation engine
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public SerializableModuleId(SerializationInfo info, StreamingContext context)
        {
            ReferenceKeys = (List<Tuple<string, int, int, byte[]>>)info.GetValue("referenceKey", typeof(List<Tuple<string, int, int, byte[]>>));
        }
    }

    /// <summary>
    /// Class for handling ReferenceKey byte[] in a typesafe manner.
    /// </summary>
    public class ModuleReferenceKeys// : ISerializableId<List<Tuple<string, int, int, byte[]>>>
    {
        public List<Tuple<string, int, int, byte[]>> Id { get; set; }

        public ModuleReferenceKeys()
        {
            Id = new List<Tuple<string, int, int, byte[]>> { }; ;
        }

        public ModuleReferenceKeys(List<Tuple<string, int, int, byte[]>> referenceKey)
        {
            this.Id = referenceKey;
        }

    }

    public class ReferenceKeyBinderModule //: IObjectBinder
    {
        private const string INVENTOR_TRACE_ID = "{097338D8-7FD3-42c5-9905-272147594D38}-INVENTOR";

        //private IContextIndexer _contextIndexer;
        private IContextManager _contextManager;

        public ReferenceKeyBinderModule(IContextManager contextManager)
        {
            //_contextIndexer = contextIndexer;
            _contextManager = contextManager;
        }


        public ISerializableId<T> GetObjectKey<T>()  
        {
            ISerializable traceData = TraceUtils.GetTraceData(INVENTOR_TRACE_ID);

            SerializableModuleId id = traceData as SerializableModuleId;
            if (id == null)
                return null;

            var traceDataRefKey = id.ReferenceKeys;
            return new ModuleReferenceKeys(traceDataRefKey) as ISerializableId<T>;
        }

        public bool GetObjectFromTrace<T>(int moduleNumber, int constraintIndex, ReferenceKeyManager refKeyManager, out T e)
        {

            if (GetObjectKey<T>() != null)
            {
                //List<Tuple<string, int, int, byte[]>> refKeys = GetObjectKey<T>().ObjectKey;
                List<Tuple<string, int, int, byte[]>> refKeys = GetObjectKey<List<Tuple<string, int, int, byte[]>>>().Id;
                Tuple<string, int, int, byte[]> matchedData = refKeys.Where(p => p.Item1 == typeof(T).ToString())
                                                                      .Where(q => q.Item2 == moduleNumber)
                                                                      .Where(r => r.Item3 == constraintIndex)
                                                                      .FirstOrDefault();

                if (matchedData != null && TryBindReferenceKey<T>(matchedData.Item4, refKeyManager, out e))
                {
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

        public void SetObjectForTrace<T>(int moduleNumber,
                                                int constraintIndex, 
                                                dynamic inventorObject,
                                                Func<List<Tuple<string, int, int, byte[]>>, 
                                                          Tuple<string, int, int, byte[]>, 
                                                          List<Tuple<string, int, int, byte[]>>> referenceKeysEvaluator)
        {
            byte[] refKey = new byte[] { };
            SerializableModuleId id = new SerializableModuleId();
            //SetObjectForTrace has been called and we need to check if there is anything it the slot.
            if (GetObjectKey<T>() != null)
            {
                //List<Tuple<string, int, int, byte[]>> refKeys = GetObjectKey<T>().ObjectKey;
                List<Tuple<string, int, int, byte[]>> refKeys = GetObjectKey<List<Tuple<string, int, int, byte[]>>>().Id;
                inventorObject.GetReferenceKey(ref refKey, 0);
                Tuple<string, int, int, byte[]> refKeyTuple = new Tuple<string, int, int, byte[]>(typeof(T).ToString(), moduleNumber, constraintIndex, refKey);
                List<Tuple<string, int, int, byte[]>> modifiedKeys = referenceKeysEvaluator(refKeys, refKeyTuple);
                id.ReferenceKeys = modifiedKeys;
                TraceUtils.SetTraceData(INVENTOR_TRACE_ID, id);
            }

            else
	        {           
                inventorObject.GetReferenceKey(ref refKey, 0);
                Tuple<string, int, int, byte[]> refKeyTuple = new Tuple<string, int, int, byte[]>(typeof(T).ToString(), moduleNumber, constraintIndex, refKey);
                id.ReferenceKeys = new List<Tuple<string, int, int, byte[]>> { refKeyTuple };
                TraceUtils.SetTraceData(INVENTOR_TRACE_ID, id);
	        }         
        }

        public static bool TryBindReferenceKey<T>(byte[] key, ReferenceKeyManager refKeyManager, out T e)
        {
            //if (ReferenceManager.KeyManager == null)
            //{
            //    InventorPersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)InventorPersistenceManager.InventorApplication.ActiveDocument;
            //    ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
           // }

            try
            {
                object outType = null;
                //int keyContext;
                byte[] keyContextArray = new byte[] { };
                //TODO: This will not work with BRep objects.  Inventor doesn't care about the KeyContext for anything else.
                //KeyContext is a long.  May be good to have a different set of methods for BRep objects to avoid storing this 
                //additional information when it isn't needed.
                //keyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                //ReferenceManager.KeyContext = keyContext;
                T invObject = (T)refKeyManager.BindKeyToObject(ref key, 0, out outType);
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
