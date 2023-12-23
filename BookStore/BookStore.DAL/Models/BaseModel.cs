using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class BaseModel
{
    [Key]
    public int Id { get; set; }
}