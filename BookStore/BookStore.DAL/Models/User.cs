using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class User: BaseModel
{
    [Required, MaxLength(50)]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; } 
    public UserRole Role { get; set; }
    public virtual IEnumerable<Review>? Reviews { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
    public virtual Cart? Cart { get; set; }
}

public enum UserRole
{
    Customer,
    Admin
}