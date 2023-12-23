using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Book: BaseModel
{
    public string Name { get; set; }
    public int AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public DateTime PublishDate { get; set; }
    public int PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
    [Range(0, Int32.MaxValue)]
    public float Price { get; set; }
    public virtual IEnumerable<Review>? Reviews { get; set; }
    public float TotalRating { get; set; }
    public virtual IEnumerable<BookCategory>? Categories { get; set; }
    public virtual IEnumerable<OrderItem>? OrderItems { get; set; } 
    public virtual IEnumerable<CartItem>? CartItems { get; set; }
}