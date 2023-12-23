using System.ComponentModel.DataAnnotations;

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
    public virtual User User { get; set; }
    public string Address { get; set; }
    public virtual IEnumerable<OrderItem>? OrderItems { get; set; }
    public DateTime OrderDateTime { get; set; }
    public OrderState OrderState { get; set; }
    [Range(0, Int32.MaxValue)]
    public float TotalPrice { get; set; }
}