namespace BookStore.BLL.DTOs.Order;

public class OrderItemDto: BaseDto
{
    public int BookId { get; set; }
    public int OrderId { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
}