namespace EasyRepository.Sample.Specs;

using Ardalis.Specification;
using Entities;

/// <summary>
///     Order by author name specification
/// </summary>
public sealed class AuthorOrderByNameSpec : Specification<Author>
{
    /// <inheritdoc />
    public AuthorOrderByNameSpec(string name)
    {
        this.Query.ApplyBaseRules()
            .ApplyByName(name)
            .OrderBy(o => o.Name);
    }
}

public static class AuthorSpecification
{
    public static ISpecificationBuilder<Author> ApplyBaseRules(this ISpecificationBuilder<Author> specificationBuilder)
    {
        specificationBuilder.Include(x => x.Books);

        return specificationBuilder;
    }

    public static ISpecificationBuilder<Author> ApplyByName(this ISpecificationBuilder<Author> specificationBuilder, string name)
    {
        specificationBuilder.Where(a => a.Name.Contains(name));

        return specificationBuilder;
    }
}