using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Cart: BaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual IEnumerable<CartItem>? CartItems { get; set; }
    [Range(0, Int32.MaxValue)]
    public float TotalPrice { get; set; }
}