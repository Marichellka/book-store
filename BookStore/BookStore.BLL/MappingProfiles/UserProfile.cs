using AutoMapper;
using BookStore.BLL.DTOs.User;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<NewUserDto, User>();
        CreateMap<NewUserDto, UserDto>();
    }
}