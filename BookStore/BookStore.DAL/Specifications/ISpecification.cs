using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications;

public interface ISpecification<TModel> where TModel: BaseModel
{
    Expression<Func<TModel, bool>> SpecificationExpression { get; }
}