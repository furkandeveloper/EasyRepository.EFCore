using AutoFilterer.Attributes;
using AutoFilterer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Dtos.Request
{
    [PossibleSortings("Name", "Surname","CreationDate", "ModificationDate", "DeletionDate")]
    public class AuthorFilterDto : PaginationFilterBase
    {
        public AuthorFilterDto()
        {
            this.Sort = "Name";
            this.SortBy = AutoFilterer.Enums.Sorting.Descending;
        }

        [ToLowerContainsComparison]
        public string Name { get; set; }

        [ToLowerContainsComparison]
        public string Surname { get; set; }

        public Range<DateTime> CreationDate { get; set; }

        public Range<DateTime> ModificationDate { get; set; }
        public Range<DateTime> DeletionDate { get; set; }
    }
}
