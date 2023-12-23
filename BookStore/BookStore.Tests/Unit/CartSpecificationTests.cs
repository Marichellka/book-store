using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Cart;

namespace BookStore.Tests.Unit;

public class CartSpecificationTests : SpecificationTestBase
{
    [Test]
    public void CartItemsSpecification_Satisfies()
    {
        CartItemsSpecification specification = new(cartId: 1);

        bool result = IsSatisfiedBy(specification, new CartItem { CartId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void CartItemsSpecification_DoesNotSatisfy()
    {
        CartItemsSpecification specification = new(cartId: 1);

        bool result = IsSatisfiedBy(specification, new CartItem { CartId = 2 });

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserCartSpecification_Satisfies()
    {
        UserCartSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Cart { UserId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserCartSpecification_DoesNotSatisfy()
    {
        UserCartSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Cart { UserId = 2 });

        Assert.That(result, Is.False);
    }
}