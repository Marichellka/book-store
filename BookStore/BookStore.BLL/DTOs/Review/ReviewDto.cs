namespace BookStore.BLL.DTOs.Review;

public class ReviewDto: BaseDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public float Rating { get; set; }
    public string? TextReview { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not ReviewDto)
            return false;
        return Equals((ReviewDto)obj);
    }
    
    protected bool Equals(ReviewDto other)
    {
        return base.Equals(other) && UserId == other.UserId && BookId == other.BookId;
    }
}