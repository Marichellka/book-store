using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Orders;

public class UsersOrdersSpecification: ISpecification<Order>
{
    private readonly int _userId;

    public UsersOrdersSpecification(int userId)
    {
        _userId = userId;
    }

    public Expression<Func<Order, bool>> SpecificationExpression => order => order.UserId == _userId;
}