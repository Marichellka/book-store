using BookStore.DAL.Models;
using BookStore.DAL.Specifications;

namespace BookStore.Tests.Specification;

public class SpecificationTestBase
{
    protected bool IsSatisfiedBy<T>(ISpecification<T> specification, T item)
        where T : BaseModel
    {
        return specification.SpecificationExpression.Compile().Invoke(item);
    }
}