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
            where T :  ComponentOccurrence //how can this be constrained and work all the time
            //It is so convenient to Element as a common base in Revit.
        {
            try
            {
                
            }
            catch
            {
                
            }
        }
    }
}
