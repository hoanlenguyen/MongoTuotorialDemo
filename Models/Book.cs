using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoTutorialDemo.Models.BaseEntities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MongoTutorialDemo.Models
{
    public class Book : MongoEntity
    {        
        [BsonElement("Name")]
        [JsonProperty("name")]
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