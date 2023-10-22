namespace BookStore.DAL.Models;

public class Cart: BaseModel
{
    public User User { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
    public float TotalPrice { get; set; }
}