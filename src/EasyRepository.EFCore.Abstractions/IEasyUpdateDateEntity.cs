namespace EasyRepository.EFCore.Abstractions;

using System;

/// <summary>
///     This interface implemented Modification Date property for entity
/// </summary>
public interface IEasyUpdateDateEntity
{
    /// <summary>
    ///     Modification Date
    /// </summary>
    public DateTime? ModificationDate { get; set; }
}