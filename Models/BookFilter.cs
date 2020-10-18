using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Models
{
    public class BookFilter
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MainGenre { get; set; }

        public List<string> SubGenres { get; set; } = new List<string>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Publisher { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Rate { get; set; }
    }
}
