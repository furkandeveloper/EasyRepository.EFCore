using EasyRepository.EFCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Entities
{
    public class Author : EasyBaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
