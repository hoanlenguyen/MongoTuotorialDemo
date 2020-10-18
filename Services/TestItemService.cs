using System.Collections.Generic;

namespace MongoTutorialDemo.Services
{
    public class TestItemService
    {
        private readonly ProviderManager providerManager;

        public TestItemService(ProviderManager providerManager)
        {
            this.providerManager = providerManager;
        }

        public IEnumerable<TestItem> GetTestItems() => providerManager.GetTestItems();
    }
}