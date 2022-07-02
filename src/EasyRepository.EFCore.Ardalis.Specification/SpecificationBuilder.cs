using System.Linq;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace EasyRepository.EFCore.Ardalis.Specification;

public static class SpecificationBuilder
{
    public static IQueryable<TEntity> Build<TEntity>(IQueryable<TEntity> entity, ISpecification<TEntity> specification) where TEntity : class, new()
    {
        return SpecificationEvaluator.Default.GetQuery(entity, specification);
    }
}