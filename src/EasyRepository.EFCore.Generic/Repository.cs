namespace EasyRepository.EFCore.Generic;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Enums;
using AutoFilterer.Extensions;
using AutoFilterer.Types;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

/// <summary>
///     This class contains implementations of repository functions
/// </summary>
internal sealed class Repository : IRepository
{
    private readonly DbContext _context;
    // public event Action<DbContext> SavingChanges = _ => { };

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="context">
    ///     Database Context <see cref="DbContext" />
    /// </param>
    public Repository(DbContext context)
    {
        this._context = context;
    }

    public TEntity Add<TEntity>(TEntity entity)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .Add(entity);

        return entity;
    }

    public TEntity Add<TEntity, TPrimaryKey>(TEntity entity)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.CreationDate = DateTime.UtcNow;
        this._context.Set<TEntity>()
            .Add(entity);

        return entity;
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await this._context.Set<TEntity>()
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return entity;
    }

    public async Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.CreationDate = DateTime.UtcNow;
        await this._context.Set<TEntity>()
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return entity;
    }

    public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .AddRange(entities);

        return entities;
    }

    public IEnumerable<TEntity> AddRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entities.ToList()
            .ForEach(x => x.CreationDate = DateTime.UtcNow);
        this._context.Set<TEntity>()
            .AddRange(entities);

        return entities;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await this._context.Set<TEntity>()
            .AddRangeAsync(entities, cancellationToken)
            .ConfigureAwait(false);

        return entities;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entities.ToList()
            .ForEach(x => x.CreationDate = DateTime.UtcNow);
        await this._context.Set<TEntity>()
            .AddRangeAsync(entities, cancellationToken)
            .ConfigureAwait(false);

        return entities;
    }

    public bool Any<TEntity>(Expression<Func<TEntity, bool>> anyExpression)
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .Any(anyExpression);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var result = await this._context.Set<TEntity>()
            .AnyAsync(anyExpression, cancellationToken)
            .ConfigureAwait(false);

        return result;
    }

    public int Count<TEntity>()
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .Count();
    }

    public int Count<TEntity>(Expression<Func<TEntity, bool>> whereExpression)
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .Where(whereExpression)
            .Count();
    }

    public async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var count = await this._context.Set<TEntity>()
            .Where(whereExpression)
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        return count;
    }

    public int Count<TEntity, TFilter>(TFilter filter)
        where TEntity : class
        where TFilter : FilterBase
    {
        return this._context.Set<TEntity>()
            .ApplyFilter(filter)
            .Count();
    }

    public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var count = await this._context.Set<TEntity>()
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        return count;
    }

    public async Task<int> CountAsync<TEntity, TFilter>(TFilter filter, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var count = await this._context.Set<TEntity>()
            .ApplyFilter(filter)
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        return count;
    }

    public void Complete()
    {
        this._context.SaveChanges();
    }


    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await this._context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public void HardDelete<TEntity>(TEntity entity)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public void HardDelete<TEntity>(object id)
        where TEntity : class
    {
        var entity = this._context.Set<TEntity>()
            .Find(id);
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public void HardDelete<TEntity, TPrimaryKey>(TEntity entity)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public void HardDelete<TEntity, TPrimaryKey>(TPrimaryKey id)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        var entity = this._context.Set<TEntity>()
            .FirstOrDefault(this.GenerateExpression<TEntity>(id));
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .Remove(entity);

        return Task.CompletedTask;
    }

    public async Task HardDeleteAsync<TEntity>(object id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var entity = await this._context.Set<TEntity>()
            .FirstOrDefaultAsync(this.GenerateExpression<TEntity>(id), cancellationToken)
            .ConfigureAwait(false);
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public Task HardDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        this._context.Set<TEntity>()
            .Remove(entity);

        return Task.CompletedTask;
    }

    public async Task HardDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        var entity = await this._context.Set<TEntity>()
            .FirstOrDefaultAsync(this.GenerateExpression<TEntity>(id), cancellationToken)
            .ConfigureAwait(false);
        this._context.Set<TEntity>()
            .Remove(entity);
    }

    public TEntity Replace<TEntity>(TEntity entity)
        where TEntity : class
    {
        this._context.Entry(entity)
            .State = EntityState.Modified;

        return entity;
    }

    public TEntity Replace<TEntity, TPrimaryKey>(TEntity entity)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        this._context.Entry(entity)
            .State = EntityState.Modified;

        return entity;
    }

    public Task<TEntity> ReplaceAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        this._context.Entry(entity)
            .State = EntityState.Modified;

        return Task.FromResult(entity);
    }

    public Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        this._context.Entry(entity)
            .State = EntityState.Modified;

        return Task.FromResult(entity);
    }

    public void SoftDelete<TEntity, TPrimaryKey>(TEntity entity)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.IsDeleted = true;
        entity.DeletionDate = DateTime.UtcNow;
        this.Replace<TEntity, TPrimaryKey>(entity);
    }

    public void SoftDelete<TEntity, TPrimaryKey>(TPrimaryKey id)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        var entity = this._context.Set<TEntity>()
            .FirstOrDefault(this.GenerateExpression<TEntity>(id));
        entity.IsDeleted = true;
        entity.DeletionDate = DateTime.UtcNow;
        this.Replace<TEntity, TPrimaryKey>(entity);
    }

    public async Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.IsDeleted = true;
        entity.DeletionDate = DateTime.UtcNow;
        await this.ReplaceAsync<TEntity, TPrimaryKey>(entity, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task SoftDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        var entity = await this._context.Set<TEntity>()
            .FirstOrDefaultAsync(this.GenerateExpression<TEntity>(id), cancellationToken)
            .ConfigureAwait(false);

        entity.IsDeleted = true;
        entity.DeletionDate = DateTime.UtcNow;
        await this.ReplaceAsync<TEntity, TPrimaryKey>(entity, cancellationToken)
            .ConfigureAwait(false);
    }

    public TEntity Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .Update(entity);

        return entity;
    }

    public TEntity Update<TEntity, TPrimaryKey>(TEntity entity)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        this._context.Set<TEntity>()
            .Update(entity);

        return entity;
    }

    public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .Update(entity);

        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        this._context.Set<TEntity>()
            .Update(entity);

        return Task.FromResult(entity);
    }

    public IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .UpdateRange(entities);

        return entities;
    }

    public IEnumerable<TEntity> UpdateRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entities.ToList()
            .ForEach(a => a.ModificationDate = DateTime.UtcNow);
        this._context.Set<TEntity>()
            .UpdateRange(entities);

        return entities;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        this._context.Set<TEntity>()
            .UpdateRange(entities);

        return entities;
    }


    public async Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entities.ToList()
            .ForEach(a => a.ModificationDate = DateTime.UtcNow);
        this._context.Set<TEntity>()
            .UpdateRange(entities);

        return entities;
    }

    /// <inheritdoc />
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public IQueryable<TEntity> GetQueryable<TEntity>()
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .AsQueryable();
    }

    public IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter)
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .Where(filter);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity>(EfTrackingOptions asNoTracking)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ToList();
    }

    /// <inheritdoc />
    public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(EfTrackingOptions asNoTracking, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Select(projectExpression)
            .ToList();
    }

    /// <inheritdoc />
    public List<TProjected> GetMultiple<TEntity, TProjected>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .ToList();
    }

    /// <inheritdoc />
    public List<TProjected> GetMultiple<TEntity, TProjected>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity>(EfTrackingOptions asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(
        bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(
        EfTrackingOptions asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TProjected> GetMultiple<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .ToList();
    }

    /// <inheritdoc />
    public List<TProjected> GetMultiple<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity, TFilter>(bool asNoTracking, TFilter filter)
        where TEntity : class
        where TFilter : FilterBase
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter)
        where TEntity : class
        where TFilter : FilterBase
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TEntity> GetMultiple<TEntity, TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    /// <inheritdoc />
    public List<TEntity> GetMultiple<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(
        bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(
        EfTrackingOptions asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .Select(projectExpression)
            .ToList();
    }

    /// <inheritdoc />
    public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter)
            .Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .ToList();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TEntity GetSingle<TEntity>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TProjected GetSingle<TEntity, TProjected, TFilter>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetSingle<TEntity, TProjected, TFilter>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetSingleAsync<TEntity>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TEntity GetSingle<TEntity>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetSingleAsync<TEntity>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetSingleAsync<TEntity>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public TProjected GetSingle<TEntity, TProjected>(EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        return this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression)
            .Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetSingle<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public TProjected GetSingle<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected>(
        bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(whereExpression);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetSingle<TEntity, TFilter>(bool asNoTracking, TFilter filter)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TEntity GetSingle<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return queryable.FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetSingleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetSingleAsync<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetSingle<TEntity, TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TEntity GetSingle<TEntity, TFilter>(EfTrackingOptions asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetSingleAsync<TEntity, TFilter>(
        bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetSingleAsync<TEntity, TFilter>(
        EfTrackingOptions asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetSingle<TEntity, TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public TProjected GetSingle<TEntity, TProjected, TFilter>(EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(
        EfTrackingOptions asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }


    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(
        bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
        where TEntity : class
        where TFilter : FilterBase
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .ApplyFilter(filter);
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetById<TEntity>(bool asNoTracking, object id)
        where TEntity : class
    {
        return this._context.Set<TEntity>()
            .FirstOrDefault(this.GenerateExpression<TEntity>(id));
    }

    /// <inheritdoc />
    public TEntity GetById<TEntity>(EfTrackingOptions asNoTracking, object id)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .FirstOrDefault(this.GenerateExpression<TEntity>(id));

        return queryable;
    }


    /// <inheritdoc />
    public TProjected GetById<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .FirstOrDefaultAsync(this.GenerateExpression<TEntity>(id), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync<TEntity>(EfTrackingOptions asNoTracking, object id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await this.FindQueryable<TEntity>(asNoTracking)
            .FirstOrDefaultAsync(this.GenerateExpression<TEntity>(id), cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TEntity GetById<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    /// <inheritdoc />
    public TEntity GetById<TEntity>(EfTrackingOptions asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return queryable.FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TEntity> GetByIdAsync<TEntity>(
        bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync<TEntity>(
        EfTrackingOptions asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return await queryable.FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetById<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public TProjected GetById<TEntity, TProjected>(EfTrackingOptions asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }

    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(
        bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(
        EfTrackingOptions asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }
    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public TProjected GetById<TEntity, TProjected>(
        bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return queryable.Select(projectExpression)
            .FirstOrDefault();
    }
    [Obsolete("The boolean option for 'asNoTracking' is obsolete. Please use the Enum.EfTrackingOptions method instead of the boolean version.")]
    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(
        bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression,
        Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var queryable = this.FindQueryable<TEntity>(asNoTracking)
            .Where(this.GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);

        return await queryable.Select(projectExpression)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private IQueryable<TEntity> FindQueryable<TEntity>(EfTrackingOptions asNoTracking)
        where TEntity : class
    {
        var queryable = this.GetQueryable<TEntity>();
        if (asNoTracking.HasNoTracking())
        {
            queryable = queryable.AsNoTracking();
        }

        return queryable;
    }

    private IQueryable<TEntity> FindQueryable<TEntity>(bool asNoTracking)
        where TEntity : class
    {
        var queryable = this.GetQueryable<TEntity>();
        if (asNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        return queryable;
    }

    private Expression<Func<TEntity, bool>> GenerateExpression<TEntity>(object id)
    {
        var type = this._context.Model.FindEntityType(typeof(TEntity));
        var pk = type.FindPrimaryKey()
            .Properties.Select(s => s.Name)
            .FirstOrDefault();
        var pkType = type.FindPrimaryKey()
            .Properties.Select(p => p.ClrType)
            .FirstOrDefault();

        var value = Convert.ChangeType(id, pkType, CultureInfo.InvariantCulture);

        var pe = Expression.Parameter(typeof(TEntity), "entity");
        var me = Expression.Property(pe, pk);
        var constant = Expression.Constant(value, pkType);
        var body = Expression.Equal(me, constant);
        var expression = Expression.Lambda<Func<TEntity, bool>>(body, pe);

        return expression;
    }
}