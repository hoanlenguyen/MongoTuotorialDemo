using Newtonsoft.Json;

namespace MongoTutorialDemo.Models.Paging
{
    public class PagingRequest
    {
        [JsonProperty(PropertyName = "currentPage")]
        public int? CurrentPage { get; set; }

        [JsonProperty(PropertyName = "itemsPerPage")]
        public int? ItemsPerPage { get; set; }       
    }
}