namespace BookStore.BLL.DTOs.Order;

public class NewOrderDto
{
    public int UserId { get; set; }
    public string Address { get; set; }
    public float TotalPrice { get; set; }
}