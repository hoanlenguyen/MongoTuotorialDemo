using System.Collections;

namespace MongoTutorialDemo.Models.Paging
{
    public class PagingResult
    {
        public long MaxItemCount { get; set; }

        public int? CurrentPage { get; set; }

        public int? ItemsPerPage { get; set; }

        public IEnumerable Items { get; set; }

        public bool HasNextPage => MaxItemCount >= CurrentPage * ItemsPerPage;

        public bool HasPreviousPage => CurrentPage > 1;
    }
}