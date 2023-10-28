namespace BookStore.BLL.DTOs.Book;

public class NewBookDto
{
    public string Name { get; set; }
    public int AuthorId { get; set; }
    public DateOnly PublishDate { get; set; }
    public int PublisherId { get; set; }
    public int CountAvailable { get; set; }
    public float Price { get; set; }
}