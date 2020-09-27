using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MongoTutorialDemo.Models
{
    public class Book : Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [MaxLength(24)]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string BookName { get; set; }

        public string MainGenre { get; set; }

        public List<string> SubGenres { get; set; } = new List<string>();

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("book_image")]
        public string BookCoverUrl { get; set; }

        public decimal Rate { get; set; }
    }
}