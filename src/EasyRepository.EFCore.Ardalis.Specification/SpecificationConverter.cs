namespace EasyRepository.EFCore.Ardalis.Specification;

using System.Linq;
using global::Ardalis.Specification;
using global::Ardalis.Specification.EntityFrameworkCore;

/// <summary>
///     Specification Builder
/// </summary>
public static class SpecificationConverter
{
    /// <summary>
    ///     This method convert specification object to queryable object.
    /// </summary>
    /// <param name="entity">
    ///     Entity
    /// </param>
    /// <param name="specification">
    ///     Specification object
    /// </param>
    /// <typeparam name="TEntity">
    ///     Entity
    /// </typeparam>
    /// <returns>
    ///     <see cref="IQueryable{TEntity}" />
    /// </returns>
    public static IQueryable<TEntity> Convert<TEntity>(IQueryable<TEntity> entity, ISpecification<TEntity> specification)
        where TEntity : class
    {
        return SpecificationEvaluator.Default.GetQuery(entity, specification);
    }
}