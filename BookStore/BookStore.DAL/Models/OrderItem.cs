namespace BookStore.DAL.Models;

public class OrderItem: BaseModel
{
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
}