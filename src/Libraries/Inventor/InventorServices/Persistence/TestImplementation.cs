using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    class TestImplementation : ITestInterface
    {
        string time = "party time";

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                value = time;
            }
        }

        public TestImplementation()
        {

        }
    }
}
