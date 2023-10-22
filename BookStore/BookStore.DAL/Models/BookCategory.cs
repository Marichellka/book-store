namespace BookStore.DAL.Models;

public class BookCategory: BaseModel
{
    public Book Book { get; set; }
    public Category Category { get; set; }
}