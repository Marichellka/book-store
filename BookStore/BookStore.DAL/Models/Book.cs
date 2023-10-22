namespace BookStore.DAL.Models;

public class Book: BaseModel
{
    public string Name { get; set; }
    public Author Author { get; set; }
    public DateOnly PublishDate { get; set; } // change to year ?
    public Publisher Publisher { get; set; }
    public int CountAvailable { get; set; } 
    public float Price { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public float TotalRating { get; set; }
}