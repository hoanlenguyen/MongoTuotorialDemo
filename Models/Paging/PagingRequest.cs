using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Models.Paging
{
    public class PagingRequest
    {
        [DefaultValue(1)]
        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [DefaultValue(10)]
        [JsonProperty(PropertyName = "itemsPerPage")]
        public int ItemsPerPage { get; set; }
    }
}
