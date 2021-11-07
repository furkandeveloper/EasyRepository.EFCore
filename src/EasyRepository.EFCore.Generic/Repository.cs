using AutoFilterer.Types;
using EasyRepository.EFCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRepository.EFCore.Generic
{
    /// <summary>
    /// This class includes implementations of repository functions
    /// </summary>
    internal class Repository : IRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">
        /// Database Context <see cref="DbContext"/>
        /// </param>
        public Repository(DbContext context)
        {
            this.context = context;
        }

        public virtual TEntity Add<TEntity>(TEntity entity) where TEntity : class, new()
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            entity.CreationDate = DateTime.UtcNow;
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            await context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            entity.CreationDate = DateTime.UtcNow;
            await context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> AddRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> anyExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public int Count<TEntity>() where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> whereExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public int Count<TEntity, TFilter>(TFilter filter)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync<TEntity, TFilter>(TFilter filter, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TEntity>(bool asNoTracking, object id) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TEntity>(bool asNoTracking, object id, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TProjected GetById<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TProjected GetById<TEntity, TProjected>(bool asNoTracking, object id, IIncludableQueryable<TEntity, TProjected> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity GetByIdAsync<TEntity>(bool asNoTracking, object id, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, IIncludableQueryable<TEntity, TProjected> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity, TFilter>(bool asNoTracking, TFilter filter)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetMultiple<TEntity, TFilter>(bool asNoTracking, TFilter filter, IIncludableQueryable<TEntity, object> includeExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public List<TProjected> GetMultiple<TEntity, TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetMultipleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjected>> GetMultipleAsync<TEntity, TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TProjected GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TProjected GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity GetSingle<TEntity, TFilter>(bool asNoTracking, TFilter filter)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public TEntity GetSingle<TEntity, TFilter>(bool asNoTracking, TFilter filter, IIncludableQueryable<TEntity, object> includeExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public TProjected GetSingle<TEntity, TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public TProjected GetSingle<TEntity, TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync<TEntity, TFilter>(bool asNoTracking, TFilter filter, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public Task<TProjected> GetSingleAsync<TEntity, TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, IIncludableQueryable<TEntity, object> includeExpression, CancellationToken cancellationToken = default)
            where TEntity : class, new()
            where TFilter : FilterBase
        {
            throw new NotImplementedException();
        }

        public void HardDelete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public void HardDelete(object id)
        {
            throw new NotImplementedException();
        }

        public void HardDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public void HardDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public TEntity Replace<TEntity>(TEntity entity) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity Replace<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ReplaceAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public void SoftDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public void SoftDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public TEntity Update<TEntity, TPrimaryKey>(TEntity entity) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> UpdateRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }
    }
}
