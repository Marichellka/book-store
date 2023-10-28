namespace BookStore.BLL.DTOs.Category;

public class CategoryDto: BaseDto
{
    public string Name { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not CategoryDto)
            return false;
        return Equals((CategoryDto)obj);
    }
    
    protected bool Equals(CategoryDto other)
    {
        return base.Equals(other) && Name == other.Name;
    }
}