using Ardalis.Specification;
using EasyRepository.Sample.Entities;

namespace EasyRepository.Sample.Specs
{
    /// <summary>
    /// Author By Name specification
    /// </summary>
    public sealed class AuthorByNameSpec : Specification<Author>
    {
        /// <inheritdoc />
        public AuthorByNameSpec(string name)
        {
            Query.Where(c => c.Name == name);
        }
    }
}