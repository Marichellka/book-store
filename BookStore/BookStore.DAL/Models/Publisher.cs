namespace BookStore.DAL.Models;

public class Publisher: BaseModel
{
    public string Name { get; set; }
    public virtual IEnumerable<Book> Books { get; set; }
}