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

        public static long KeyContext { get; set; }

        //airball
        //public static dynamic UIDocument { get; set; }

        //public static void SetActiveDocument()
        //{
        //    if (InventorApplication.ActiveDocument is AssemblyDocument)
        //    {
        //        UIDocument = (AssemblyDocument)InventorApplication.ActiveDocument;
        //    }
        //    else if (InventorApplication.ActiveDocument is PartDocument)
        //    {
        //        UIDocument = (PartDocument)InventorApplication.ActiveDocument;
        //    }
        //}
    }
}
