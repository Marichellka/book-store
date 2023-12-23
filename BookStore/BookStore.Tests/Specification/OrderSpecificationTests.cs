using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Orders;

namespace BookStore.Tests.Specification;

public class OrderSpecificationTests : SpecificationTestBase
{
    [Test]
    public void OrderItemsSpecification_Satisfies()
    {
        OrderItemsSpecification specification = new(orderId: 1);

        bool result = IsSatisfiedBy(specification, new OrderItem { OrderId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void OrderItemsSpecification_DoesNotSatisfy()
    {
        OrderItemsSpecification specification = new(orderId: 1);

        bool result = IsSatisfiedBy(specification, new OrderItem { OrderId = 2 });

        Assert.That(result, Is.False);
    }

    [Test]
    public void UsersOrdersSpecification_Satisfies()
    {
        UsersOrdersSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Order { UserId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void UsersOrdersSpecification_DoesNotSatisfy()
    {
        UsersOrdersSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Order { UserId = 2 });

        Assert.That(result, Is.False);
    }
}
