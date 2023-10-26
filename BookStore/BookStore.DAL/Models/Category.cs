namespace BookStore.DAL.Models;

public class Category: BaseModel
{
    public string Name { get; set; }
    public IEnumerable<BookCategory> BookCategories { get; set; }
}