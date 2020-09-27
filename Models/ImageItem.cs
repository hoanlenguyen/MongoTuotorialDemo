using Newtonsoft.Json;

namespace MongoTutorialDemo.Models
{
    public class ImageItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        //[JsonProperty("width")]
        //public int Width { get; set; }

        //[JsonProperty("height")]
        //public int Height { get; set; }

        //[JsonProperty("url")]
        //public string Url { get; set; }

        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }
    }

}
