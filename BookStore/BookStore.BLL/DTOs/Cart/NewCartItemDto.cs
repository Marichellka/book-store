namespace BookStore.BLL.DTOs.Cart;

public class NewCartItemDto
{
    public int BookId { get; set; }
    public int CartId { get; set; }
    public int Count { get; set; }
}