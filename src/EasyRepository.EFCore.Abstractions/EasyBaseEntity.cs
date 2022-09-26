namespace EasyRepository.EFCore.Abstractions;

using System;

/// <summary>
///     This abstraction implemented base properties for entities
/// </summary>
/// <typeparam name="TPrimaryKey">
///     Primary Key type of the entity
/// </typeparam>
public abstract class EasyBaseEntity<TPrimaryKey> : IEasyEntity<TPrimaryKey>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
{
    /// <summary>
    ///     Creation Date <see cref="{DateTime}" />
    /// </summary>
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    ///     Primary Key <see cref="{TPrimaryKey}" />
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }

    /// <summary>
    ///     Modification Date <see cref="{DateTime}" />
    /// </summary>
    public virtual DateTime? ModificationDate { get; set; }

    /// <summary>
    ///     Deletion Date <see cref="{DateTime}" />
    /// </summary>
    public virtual DateTime? DeletionDate { get; set; }

    /// <summary>
    ///     Is Deleted <see cref="{Boolean}" />
    /// </summary>
    public virtual bool IsDeleted { get; set; }
}