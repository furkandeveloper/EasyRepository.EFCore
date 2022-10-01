namespace EasyRepository.EFCore.Generic;

using Abstractions;

/// <summary>
///     Implementation of Unit of work pattern
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IRepository repository)
    {
        this.Repository = repository;
    }

    public IRepository Repository { get; }
}