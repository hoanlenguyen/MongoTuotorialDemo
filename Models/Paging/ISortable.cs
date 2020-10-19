using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Models.Paging
{
    public interface ISortable
    {
        public string SortFieldName { get; set; }

        public bool IsAscending { get; set; }
    }
}
