using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
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
        void HardDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : EasyBaseEntity<TPrimaryKey>;

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


        /// <summary>
        /// This method provides entity queryable version. In additional this method returns <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <returns>
        /// Returns <see cref="IQueryable{TEntity}"/>
        /// </returns>
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{TDelegate}"/> and apply filter to data source. In additional returns <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="filter">
        /// The filter to apply on the Entity.
        /// </param>
        /// <returns>
        /// Returns <see cref="IQueryable{TEntity}"/>
        /// </returns>
        IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();

        /// <summary>
        /// This method  returns List of Entity without filter. <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class, new();

        /// <summary>
        /// This method  returns List of Entity without filter async version. <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method provides without filter get all entity but you can convert it to any object you want.
        /// In additional this method takes <see cref="Expression{Func}"/> returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/>. This method performs without filter get all entity but you can convert it to any object you want
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> performs apply filter get all entity. In additional returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where expression see <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/>. This method performs apply filter get all entity. In additional returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> where expression and <see cref="Expression{Func}"/> select expression. This method performs apply filter and convert returns get all entity. In additional returns <see cref="List{TPrtojected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of Projected object
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> where expression and <see cref="Expression{Func}"/> select expression. This method performs apply filter async version and convert returns get all entity. In additional returns <see cref="List{TPrtojected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of Projected object
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method performs get all with includable entities. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="includeExpression">
        /// <see cref="IIncludableQueryable{TEntity, TProperty}"/> expression
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method performs get all with includable entities async version. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="includeExpression">
        /// <see cref="IIncludableQueryable{TEntity, TProperty}"/> expression
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/>, <see cref="Expression{Func}"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method perform get all entities with filter and includable entities. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/>, <see cref="Expression{Func}"/>, <see cref="IIncludableQueryable{TEntity, TProperty}"/> and <see cref="CancellationToken"/>. This method perform get all entities with filter and includable entities async version. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression and <see cref="Expression{Func}"/> select expression. This method perform get all projected object with filter and include entities. In additional returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new();

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression, <see cref="Expression{Func}"/> select expression and <see cref="CancellationToken"/> cancellation token. This method perform get all projected object with filter and include entities async version. In additional returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new();
    }
}
