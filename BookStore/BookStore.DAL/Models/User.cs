namespace BookStore.DAL.Models;

public class User: BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // ?
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    public Cart Cart { get; set; }
}