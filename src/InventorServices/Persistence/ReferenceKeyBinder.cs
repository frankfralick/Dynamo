using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

namespace InventorServices.Persistence
{
    public class ReferenceKeyBinder
    {
        public static bool TryBindReferenceKey<T>(byte[] key, out T e)
        //where T :  ComponentOccurrence //how can this be constrained and work all the time
        //It is so convenient to Element as a common base in Revit.
        {
            //if (InventorSettings.KeyManager == null)
            if (ReferenceManager.KeyManager == null)
            {
                //TODO Set these once, elsewhere.
                //InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
                InventorPersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)InventorPersistenceManager.InventorApplication.ActiveDocument;
                //InventorSettings.KeyManager = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager;
                ReferenceManager.KeyManager = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            try
            {
                object outType = null;
                int keyContext;
                byte[] keyContextArray = new byte[] { };

                //Eventually will need this to work for both BRep objects and all other entity types.  The
                //BRep objects like faces need a context array to be loaded with the reference key.  Saving 
                //and loading the context for non-BReps like workpoints doesn't work.  Context isn't 'ignored'
                //a new one needs to be created for each binding operation.  

                //if (InventorSettings.KeyContextArray != null)
                //{
                //    //keyContext = InventorSettings.KeyManager.LoadContextFromArray(ref keyContextArray);
                //    keyContext = InventorSettings.KeyManager.LoadContextFromArray(InventorSettings.KeyContextArray);
                //    InventorSettings.KeyContext = keyContext;
                //}
                //else //We are in a new file without bound objects.
                //{
                //    if (InventorSettings.KeyContext == null)
                //    {
                //        keyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                //        InventorSettings.KeyContext = keyContext;
                //    }

                //    //InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.SaveContextToArray((int)InventorSettings.KeyContext, ref keyContextArray);
                //    //InventorSettings.KeyContextArray = keyContextArray;
                //}

                //keyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                keyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                //InventorSettings.KeyContext = keyContext;
                ReferenceManager.KeyContext = keyContext;
                //T invObject = (T)InventorSettings.KeyManager.BindKeyToObject(ref key, (int)InventorSettings.KeyContext, out outType);
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
