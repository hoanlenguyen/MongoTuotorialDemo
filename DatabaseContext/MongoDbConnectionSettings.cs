namespace MongoTutorialDemo.DatabaseContext
{
    public interface IMongoDbConnectionSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}

namespace MongoTutorialDemo.DatabaseContext
{
    public class MongoDbConnectionSettings : IMongoDbConnectionSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}