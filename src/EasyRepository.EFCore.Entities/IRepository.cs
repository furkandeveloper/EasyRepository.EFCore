using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRepository.EFCore.Abstractions
{
    /// <summary>
    /// This interface implemented base database operation with generic repository pattern
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity insert operation. In additional this methods returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity <see cref="{TEntity}"/>
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Add<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity insert async. In additional this methods returns <see cref="Task{TEntity}"/> 
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity <see cref="{TEntity}"/>
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="EasyBaseEntity{TPrimaryKey}"/> and performs entity insert operation. In additional this methods returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of entity primary key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="EasyBaseEntity{TPrimaryKey}"/> and performs entity insert operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of entity primary key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This methods takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entities">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="IEnumerable{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation async version. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="IEnumerable{EasyBaseEntity{TPrimaryKey}}"/> and performs entity insert range operation. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="IEnumerable{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> AddRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="IEnumerable{EasyBaseEntity{TPrimaryKey}}"/> and performs entity insert range operation. In additional this methods returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of primary key
        /// </typeparam>
        /// <param name="entites">
        /// The entities to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> AddRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;
    }
}
