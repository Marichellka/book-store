namespace BookStore.DAL.Models;

public enum OrderState
{
    Processing,
    Delivering,
    Delivered
}

public class Order: BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    public string Address { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }
    public DateTime OrderDateTime { get; set; }
    public OrderState OrderState { get; set; }
    public float TotalPrice { get; set; }
}