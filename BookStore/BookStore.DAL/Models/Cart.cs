namespace BookStore.DAL.Models;

public class Cart: BaseModel
{
    public virtual User User { get; set; }
    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public float TotalPrice { get; set; }
}