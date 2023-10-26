namespace BookStore.DAL.Models;

public class CartItem: BaseModel
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
}