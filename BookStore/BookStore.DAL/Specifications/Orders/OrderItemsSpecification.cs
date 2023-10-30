using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Orders;

public class OrderItemsSpecification: ISpecification<OrderItem>
{
    private readonly int _orderId;

    public OrderItemsSpecification(int orderId)
    {
        _orderId = orderId;
    }

    public Expression<Func<OrderItem, bool>> SpecificationExpression => item => item.OrderId == _orderId;
}