using EasyRepository.EFCore.Abstractions;

namespace EasyRepository.EFCore.Generic;

/// <summary>
/// Abstraction of Unit Of Work pattern
/// </summary>
public interface IUnitOfWork
{
    IRepository Repository { get; }
}