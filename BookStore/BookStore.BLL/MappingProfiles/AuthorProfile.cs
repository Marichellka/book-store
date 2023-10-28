using AutoMapper;
using BookStore.BLL.DTOs.Author;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class AuthorProfile: Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<NewAuthorDto, Author>();
        CreateMap<NewAuthorDto, AuthorDto>();
    }
}