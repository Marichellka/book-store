using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Cart;

public class CartItemsSpecification: ISpecification<CartItem>
{
    private readonly int _cartId;

    public CartItemsSpecification(int cartId)
    {
        _cartId = cartId;
    }

    public Expression<Func<CartItem, bool>> SpecificationExpression => item => item.CartId == _cartId;
}