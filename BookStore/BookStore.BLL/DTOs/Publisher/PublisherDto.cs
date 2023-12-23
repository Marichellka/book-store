namespace BookStore.BLL.DTOs.Publisher;

public class PublisherDto: BaseDto
{
    public string Name { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not PublisherDto)
            return false;
        return Equals((PublisherDto)obj);
    }
    
    protected bool Equals(PublisherDto other)
    {
        return base.Equals(other) && Name == other.Name;
    }
}