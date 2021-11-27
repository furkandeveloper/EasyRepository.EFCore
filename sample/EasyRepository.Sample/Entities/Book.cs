using EasyRepository.EFCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Entities
{
    public class Book : EasyBaseEntity<Guid>
    {
        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public int TotalPage { get; set; }

        public virtual Author Author { get; set; }
    }
}
