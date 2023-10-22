namespace BookStore.DAL.Models;

public class OrderItem: BaseModel
{
    public Book Book { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
}