namespace EasyRepository.Sample.Dtos.Request;

using System;
using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

[PossibleSortings("Name", "Surname", "CreationDate", "ModificationDate", "DeletionDate")]
public class AuthorFilterDto : PaginationFilterBase
{
    public AuthorFilterDto()
    {
        this.Sort = "Name";
        this.SortBy = Sorting.Descending;
    }

    [ToLowerContainsComparison]
    public string Name { get; set; }

    [ToLowerContainsComparison]
    public string Surname { get; set; }

    public Range<DateTime> CreationDate { get; set; }

    public Range<DateTime> ModificationDate { get; set; }
    public Range<DateTime> DeletionDate { get; set; }
}