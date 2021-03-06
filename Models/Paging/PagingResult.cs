﻿using System.Collections;

namespace MongoTutorialDemo.Models.Paging
{
    public class PagingResult
    {
        public long MaxItemCount { get; set; }

        public int? CurrentPage { get; set; }

        public int? ItemsPerPage { get; set; }

        public IEnumerable Items { get; set; }

        public bool HasNextPage => (CurrentPage != null && ItemsPerPage != null) ?
                                    MaxItemCount >= (CurrentPage * ItemsPerPage)
                                    : false;

        public bool HasPreviousPage => CurrentPage.GetValueOrDefault() > 1;
    }
}