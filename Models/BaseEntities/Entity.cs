using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Models.BaseEntities
{
    public class Entity<T> :BaseEntity
    {
        public T Id { get; set; }
    }
}
