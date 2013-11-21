using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inventor;

namespace DynamoInventor
{
    class InventorSettings
    {
        public static Inventor.Application InventorApplication { get; set; }

        public static AssemblyDocument ActiveAssemblyDoc { get; set; }

        public static Stack<ComponentOccurrencesContainer> ComponentOccurrencesContainers
            = new Stack<ComponentOccurrencesContainer>(new[] { new ComponentOccurrencesContainer() });

        public static ReferenceKeyManager KeyManager { get; set; }
        
        public static int KeyContext { get; set; }
    }
}
