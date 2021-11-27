using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Dtos.Request
{
    public class BookRequestDto
    {
        public string Title { get; set; }

        public int TotalPage { get; set; }
    }
}
