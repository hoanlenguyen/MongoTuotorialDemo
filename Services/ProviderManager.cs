using System.Collections.Generic;

namespace MongoTutorialDemo.Services
{
    public class ProviderManager
    {
        List<ITestService> TestServices { get; }
        public ProviderManager(IEnumerable<ITestService> testServices)
        {
            TestServices = new List<ITestService>(testServices);
        }

        public IEnumerable<TestItem> GetTestItems()
        {
            foreach (var test in TestServices)
            {
                yield return new TestItem
                {
                    Name = test.ServiceName,
                    Value = test.GetValue()
                };
            }
        }
    }
}