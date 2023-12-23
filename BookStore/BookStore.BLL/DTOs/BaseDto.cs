namespace BookStore.BLL.DTOs;

public class BaseDto
{
    public int Id { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not BaseDto)
            return false;
        return Equals((BaseDto)obj);
    }
    
    protected bool Equals(BaseDto other)
    {
        return Id == other.Id;
    }

}