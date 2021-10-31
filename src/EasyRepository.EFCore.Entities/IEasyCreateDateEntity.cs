using System;

namespace EasyRepository.EFCore.Abstractions
{
    /// <summary>
    /// This interface implemented creation date for entity
    /// </summary>
    public interface IEasyCreateDateEntity
    {
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
