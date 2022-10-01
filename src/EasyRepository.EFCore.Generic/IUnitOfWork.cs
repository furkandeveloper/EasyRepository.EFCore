namespace EasyRepository.EFCore.Generic;

using Abstractions;

/// <summary>
///     Abstraction of Unit Of Work pattern
/// </summary>
public interface IUnitOfWork
{
    IRepository Repository { get; }
}