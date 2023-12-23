using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Cart;

public class UserCartSpecification: ISpecification<Models.Cart>
{
    private readonly int _userId;

    public UserCartSpecification(int userId)
    {
        _userId = userId;
    }

    public Expression<Func<Models.Cart, bool>> SpecificationExpression => cart => cart.UserId == _userId;
}