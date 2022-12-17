using System;

namespace EasyRepository.EFCore.Abstractions
{
    /// <summary>
    /// This abstraction implemented base properties for entities
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// Primary Key type of the entity
    /// </typeparam>
    public abstract class EasyBaseEntity<TPrimaryKey> : IEasyEntity<TPrimaryKey>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        /// <summary>
        /// Creation Date <see>
        ///     <cref>{DateTime}</cref>
        /// </see>
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Primary Key <see>
        ///     <cref>{TPrimaryKey}</cref>
        /// </see>
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// Modification Date <see>
        ///     <cref>{DateTime}</cref>
        /// </see>
        /// </summary>
        public virtual DateTime? ModificationDate { get; set; }

        /// <summary>
        /// Deletion Date <see>
        ///     <cref>{DateTime}</cref>
        /// </see>
        /// </summary>
        public virtual DateTime? DeletionDate { get; set; }

        /// <summary>
        /// Is Deleted <see>
        ///     <cref>{Boolean}</cref>
        /// </see>
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
