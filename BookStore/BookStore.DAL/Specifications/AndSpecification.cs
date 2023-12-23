using System.Linq.Expressions;
using BookStore.DAL.Models;
using AutoMapper.Execution;

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
        ParameterExpression parameter = Expression.Parameter(typeof(TModel));

        var body1 = e1.ReplaceParameters(parameter);
        var body2 = e2.ReplaceParameters(parameter);

        BinaryExpression andExpression = Expression.AndAlso(body1, body2);

        return Expression.Lambda<Func<TModel, bool>>(andExpression, parameter);
    }

}