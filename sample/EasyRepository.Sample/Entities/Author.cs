namespace EasyRepository.Sample.Entities;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EFCore.Abstractions;

public class Author : EasyBaseEntity<Guid>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; }
}