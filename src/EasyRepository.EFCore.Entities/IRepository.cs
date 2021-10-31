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

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity hard delete operation.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        void HardDelete<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/>. This method performs entity hard delete operation. In additional this methods returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Object"/> and performs entity hard delete operation
        /// </summary>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void HardDelete(object id);

        /// <summary>
        /// This method takes <see cref="Object"/> an <see cref="CancellationToken"/>. This method performs hard delete operation async version. In additional returns <see cref="Task"/>
        /// </summary>
        /// <param name="id">
        /// Pk of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync(object id, CancellationToken cancellationToken = default);


        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs hard delete operation
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key for Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        void HardDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs hard delete operation async version for EasyBaseEntity. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs hard delete operation by id.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void HardDelete<TEntity,TPrimaryKey>(TPrimaryKey id) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and <see cref="CancellationToken"/>. This method performs hard delete operation by id async version. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;


        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs soft delete operation
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void SoftDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs soft delete operation. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs soft delete operation by id.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void SoftDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and <see cref="CancellationToken"/>. This method performs soft delete operation by id async version. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task SoftDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs update operation. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> performs update operation. In additional returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{TResult}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for EasyBaseEntity. In additional this method returs <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Update<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for EasyBaseEntity. In additional this method returs <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns>
        Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update range operation for EasyBaseEntity. In additional this method returs <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> UpdateRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for EasyBaseEntity. In additional this method returs <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entites">
        /// The entites to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TResult}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs replace operation. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        TEntity Replace<TEntity>(TEntity entity) where TEntity : class, new();


        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs replace operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task<TEntity> ReplaceAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs replace operation for EasyBaseEntity. In additional this method returs <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Replace<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs replace operation for EasyBaseEntity. In additional this method returs <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns>
        Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;
    }
}
