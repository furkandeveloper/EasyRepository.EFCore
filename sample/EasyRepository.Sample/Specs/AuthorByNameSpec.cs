namespace EasyRepository.Sample.Specs;

using Ardalis.Specification;
using Entities;

/// <summary>
///     Author By Name specification
/// </summary>
public sealed class AuthorByNameSpec : Specification<Author>
{
    /// <inheritdoc />
    public AuthorByNameSpec(string name)
    {
        this.Query.Where(c => c.Name == name);
    }
}