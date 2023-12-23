using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Publisher: BaseModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; }
    public virtual IEnumerable<Book>? Books { get; set; }
}