using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorServices.Persistence
{
    public class TestUserClass
    {
        ITestInterface _testInterface;

        public TestUserClass(ITestInterface testInterface)
        {
            _testInterface = testInterface;
        }
    }
}
