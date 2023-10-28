namespace BookStore.BLL.DTOs.Book;

public class BookDto: BaseDto
{
    public string Name { get; set; }
    public int AuthorId { get; set; }
    public DateOnly PublishDate { get; set; }
    public int PublisherId { get; set; }
    public int CountAvailable { get; set; }
    public float Price { get; set; }
    public float TotalRating { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not BookDto)
            return false;
        return Equals((BookDto)obj);
    }

    protected bool Equals(BookDto other)
    {
        return base.Equals(other) && Name == other.Name && AuthorId == other.AuthorId &&
               PublisherId == other.PublisherId;
    }
}