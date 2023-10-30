using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications;

public class AndSpecification<TModel>: ISpecification<TModel> where TModel : BaseModel
{
    private readonly ISpecification<TModel> _lhs;
    private readonly ISpecification<TModel> _rhs;
    
    public AndSpecification(ISpecification<TModel> lhs, ISpecification<TModel> rhs)
    {
        _lhs = lhs;
        _rhs = rhs;
    }
    
    public Expression<Func<TModel, bool>> SpecificationExpression => And(_lhs.SpecificationExpression, _rhs.SpecificationExpression);
    private static Expression<Func<TModel, bool>> And(Expression<Func<TModel, bool>> e1, Expression<Func<TModel, bool>> e2)
    {
        var body = Expression.AndAlso(e1.Body, e2.Body);
        return Expression.Lambda<Func<TModel, bool>>(body, e1.Parameters[0]);
    }

}