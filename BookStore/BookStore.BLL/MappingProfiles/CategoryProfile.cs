using AutoMapper;
using BookStore.BLL.DTOs.Category;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<NewCategoryDto, Category>();
        CreateMap<NewCategoryDto, CategoryDto>();
    }
}