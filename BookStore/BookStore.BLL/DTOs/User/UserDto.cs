namespace BookStore.BLL.DTOs.User;

public class UserDto: BaseDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    
    public override bool Equals(object? obj)
    {
        if (obj is not UserDto)
            return false;
        return Equals((UserDto)obj);
    }

    protected bool Equals(UserDto other)
    {
        return base.Equals(other) && Name == other.Name && Email == other.Email;
    }
}