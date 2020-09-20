using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MongoTutorialDemo.Models
{
    public class Book : Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [MaxLength(24)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}