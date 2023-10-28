namespace BookStore.BLL.DTOs.Review;

public class NewReviewDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public float Rating { get; set; }
    public string? TextReview { get; set; }
}