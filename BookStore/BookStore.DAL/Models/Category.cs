using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Category: BaseModel
{
    [Required]
    public string Name { get; set; }
    public virtual IEnumerable<BookCategory>? BookCategories { get; set; }
}