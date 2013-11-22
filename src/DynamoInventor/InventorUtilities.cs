using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;

namespace DynamoInventor
{
    class InventorUtilities
    {
        public static bool TryBindReferenceKey<T>(byte[] key, out T e)
            //where T :  ComponentOccurrence //how can this be constrained and work all the time
            //It is so convenient to Element as a common base in Revit.
        {
            if (InventorSettings.KeyManager == null)
            {
                //TODO Set these once, elsewhere.
                InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
                InventorSettings.KeyManager = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            try
            {
                //Whoever is responsible for Inventor API documentation is failing at their job.
                //object outType = typeof(T);
                object outType = null;
                int keyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                T invObject = (T)InventorSettings.KeyManager.BindKeyToObject(ref key, keyContext, out outType);
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
