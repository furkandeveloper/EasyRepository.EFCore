using System;

namespace EasyRepository.EFCore.Abstractions
{
    /// <summary>
    /// This interface implemented Deletion Date and Is Deleted property for entity
    /// </summary>
    public interface IEasySoftDeleteEntity
    {
        /// <summary>
        /// Deletion Date
        /// </summary>
        public DateTime? DeletionDate { get; set; }

        /// <summary>
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
