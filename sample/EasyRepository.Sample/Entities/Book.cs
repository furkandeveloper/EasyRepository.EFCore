namespace EasyRepository.Sample.Entities;

using System;
using EFCore.Abstractions;

public class Book : EasyBaseEntity<Guid>
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; }

    public int TotalPage { get; set; }

    public virtual Author Author { get; set; }
}