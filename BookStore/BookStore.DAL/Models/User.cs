namespace BookStore.DAL.Models;

public class User: BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // ?
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual Cart Cart { get; set; }
}