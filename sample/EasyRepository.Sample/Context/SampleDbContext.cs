using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Context
{
    /// <summary>
    /// Sample Database Context
    /// </summary>
    public class SampleDbContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">
        /// The options to be used by a Microsoft.EntityFrameworkCore.DbContext. You normally
        ///     override Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        ///     or use a Microsoft.EntityFrameworkCore.DbContextOptionsBuilder to create instances
        ///     of this class and it is not designed to be directly constructed in your application
        ///     code.
        /// </param>
        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SampleDbContext()
        {
        }
    }
}
