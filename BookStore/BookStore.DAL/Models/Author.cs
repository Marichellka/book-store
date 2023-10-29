using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Author: BaseModel
{
    [Required, MaxLength(100)]
    public string FullName { get; set; }
    public virtual IEnumerable<Book>? Books { get; set; }
}