namespace BookStore.BLL.DTOs.Cart;

public class CartDto: BaseDto
{
    public int UserId { get; set; }
    public float TotalPrice { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not CartDto)
            return false;
        return Equals((CartDto)obj);
    }
    
    protected bool Equals(CartDto other)
    {
        return base.Equals(other) && UserId == other.UserId;
    }
}