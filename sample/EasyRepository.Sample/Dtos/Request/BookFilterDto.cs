using AutoFilterer.Attributes;
using AutoFilterer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Dtos.Request
{
    [PossibleSortings("Title", "CreationDate", "ModificationDate", "DeletionDate")]
    public class BookFilterDto : PaginationFilterBase
    {
        public BookFilterDto()
        {
            this.Sort = "Title";
            this.SortBy = AutoFilterer.Enums.Sorting.Descending;
        }

        [ToLowerContainsComparison]
        public string Title { get; set; }

        public Range<int> TotalPage { get; set; }

        public Range<DateTime> CreationDate { get; set; }

        public Range<DateTime> ModificationDate { get; set; }
        public Range<DateTime> DeletionDate { get; set; }
    }
}
