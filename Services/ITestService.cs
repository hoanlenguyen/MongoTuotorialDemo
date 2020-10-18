using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Services
{
    public interface ITestService
    {
        public string ServiceName => "Base Test";

        public int GetValue()
        {
            return 20;
        }
    }
}