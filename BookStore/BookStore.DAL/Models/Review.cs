namespace BookStore.DAL.Models;

public class Review: BaseModel
{
    public User User { get; set; }
    public Book Book { get; set; }
    public float Rating { get; set; }
    public string TextReview { get; set; }
}