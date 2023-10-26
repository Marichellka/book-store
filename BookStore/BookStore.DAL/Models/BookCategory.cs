namespace BookStore.DAL.Models;

public class BookCategory: BaseModel
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}