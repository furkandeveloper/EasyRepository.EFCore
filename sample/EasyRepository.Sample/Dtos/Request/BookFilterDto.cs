namespace EasyRepository.Sample.Dtos.Request;

using System;
using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

[PossibleSortings("Title", "CreationDate", "ModificationDate", "DeletionDate")]
public class BookFilterDto : PaginationFilterBase
{
    public BookFilterDto()
    {
        this.Sort = "Title";
        this.SortBy = Sorting.Descending;
    }

    [ToLowerContainsComparison]
    public string Title { get; set; }

    public Range<int> TotalPage { get; set; }

    public Range<DateTime> CreationDate { get; set; }

    public Range<DateTime> ModificationDate { get; set; }
    public Range<DateTime> DeletionDate { get; set; }
}