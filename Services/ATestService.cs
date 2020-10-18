using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Services
{
    public class ATestService : ITestService
    {
        public string ServiceName => "A Test";

        public int GetValue()
        {
            return 40;
        } 
    }
}
