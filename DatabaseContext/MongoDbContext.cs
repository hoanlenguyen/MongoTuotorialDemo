using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.DatabaseContext
{
    public class MongoDbContext<T> where T:class
    {   
        public IMongoCollection<T> Collection { get; private set; }
        public MongoDbContext(IMongoDbConnectionSettings settings, string collectionName = null)
        {
            var client = new MongoClient("mongodb+srv://HoanMongoDb:qwe123@hoancluster.1nhf6.azure.mongodb.net/<dbname>?retryWrites=true&w=majority");
            var database = client.GetDatabase(settings.DatabaseName);
            Collection = database.GetCollection<T>(collectionName ?? settings.CollectionName);
        }
    }
}
