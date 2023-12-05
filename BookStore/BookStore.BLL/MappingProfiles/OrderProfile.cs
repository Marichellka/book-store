using AutoMapper;
using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.DTOs.Order;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<NewOrderDto, Order>();
        CreateMap<NewOrderDto, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CartItemDto, OrderItemDto>();
        CreateMap<CartItem, OrderItem>().ForMember(x => x.Id, opt => opt.Ignore());
    }
}