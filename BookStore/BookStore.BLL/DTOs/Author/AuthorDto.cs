namespace BookStore.BLL.DTOs.Author;

public class AuthorDto: BaseDto
{
    public string FullName { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not AuthorDto)
            return false;
        return Equals((AuthorDto)obj);
    }
    
    protected bool Equals(AuthorDto other)
    {
        return base.Equals(other) && FullName == other.FullName;
    }

}