using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications;

public class FalseSpecification<TModel>: ISpecification<TModel> where TModel : BaseModel
{
    public Expression<Func<TModel, bool>> SpecificationExpression => _ => false;
}