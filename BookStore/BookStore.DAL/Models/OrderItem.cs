namespace BookStore.DAL.Models;

public class OrderItem: BaseModel
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
}