namespace BookStore.DAL.Models;

public class Review: BaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public float Rating { get; set; }
    public string TextReview { get; set; }
}