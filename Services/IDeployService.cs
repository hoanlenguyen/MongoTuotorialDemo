using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Services
{
    public interface IDeployService
    {
        public bool IsDeployed { get; set; }
    }
}
