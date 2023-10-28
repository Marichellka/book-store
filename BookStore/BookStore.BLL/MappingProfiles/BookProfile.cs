using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class BookProfile: Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<NewBookDto, Book>();
        CreateMap<NewBookDto, BookDto>();
    }
}