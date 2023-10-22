namespace BookStore.DAL.Models;

public class Author: BaseModel
{
    public string FullName { get; set; }
    public IEnumerable<Book> Books { get; set; }
}