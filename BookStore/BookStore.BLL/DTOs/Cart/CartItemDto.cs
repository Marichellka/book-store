namespace BookStore.BLL.DTOs.Cart;

public class CartItemDto: BaseDto
{
    public int BookId { get; set; }
    public int CartId { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not CartItemDto)
            return false;
        return Equals((CartItemDto)obj);
    }
    
    protected bool Equals(CartItemDto other)
    {
        return base.Equals(other) && CartId == other.CartId && BookId == other.BookId;
    }
}