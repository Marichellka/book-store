using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class OrderItem: BaseModel
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    [Range(1, Int32.MaxValue)]
    public int Count { get; set; }
    [Range(0, Int32.MaxValue)]
    public float Price { get; set; }
}