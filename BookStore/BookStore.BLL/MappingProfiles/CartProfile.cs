using AutoMapper;
using BookStore.BLL.DTOs.Cart;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class CartProfile: Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<NewCartDto, Cart>();
        CreateMap<NewCartDto, CartDto>();
    }
}