using BookStore.DAL.Models;

namespace BookStore.BLL.DTOs.Order;

public class OrderDto: BaseDto
{
    public int UserId { get; set; }
    public string Address { get; set; }
    public DateTime OrderDateTime { get; set; }
    public OrderState OrderState { get; set; }
    public float TotalPrice { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not OrderDto)
            return false;
        return Equals((OrderDto)obj);
    }
    
    protected bool Equals(OrderDto other)
    {
        return base.Equals(other) && UserId == other.UserId && OrderDateTime == other.OrderDateTime &&
               Address == other.Address;
    }
}