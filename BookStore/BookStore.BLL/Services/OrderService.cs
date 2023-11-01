using AutoMapper;
using BookStore.BLL.DTOs.Order;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class OrderService: BaseService
{
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
     public async Task<ICollection<OrderDto>> GetAll(ISpecification<Order>? specification = null)
    {
        var orders = await UnitOfWork.OrderRepository.GetAll(specification);
        return Mapper.Map<ICollection<OrderDto>>(orders);
    }

    public async Task<ICollection<OrderItemDto>> GetAllOrderItems(ISpecification<OrderItem>? specification = null)
    {
        var items = await UnitOfWork.OrderItemRepository.GetAll(specification);
        return Mapper.Map<ICollection<OrderItemDto>>(items);
    }

    public async Task<OrderDto> GetById(int id)
    {
        var order = await UnitOfWork.OrderRepository.GetById(id) ??
                        throw new NotFoundException(nameof(Order), id);

        return Mapper.Map<OrderDto>(order);
    }
    
    public async Task<OrderDto> Create(NewOrderDto order)
    {
        var user = await UnitOfWork.UserRepository.GetById(order.UserId) ?? 
                   throw new NotFoundException(nameof(User), order.UserId);

        var cart = user.Cart ?? throw new NotFoundException($"No Cart Found for User ({user.Id})");
        var orderEntity = Mapper.Map<Order>(order);
        orderEntity.TotalPrice = cart.TotalPrice;
        orderEntity.OrderDateTime = DateTime.Now;
        orderEntity.OrderState = OrderState.Processing;

        await UnitOfWork.OrderRepository.Add(orderEntity);
        await UnitOfWork.SaveChangesAsync();

        var items = Mapper.Map<ICollection<OrderItem>>(cart.CartItems);
        foreach (var item in items)
        {
            var book = await UnitOfWork.BookRepository.GetById(item.BookId) ??
                       throw new NotFoundException(nameof(Book), item.BookId);
            await UnitOfWork.BookRepository.Update(book);
            
            item.OrderId = orderEntity.Id;
            await UnitOfWork.OrderItemRepository.Add(item);
        }

        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<OrderDto>(orderEntity);
    }

    public async Task<OrderDto> Update(OrderDto orderDto)
    {
        if(await UnitOfWork.OrderRepository.GetById(orderDto.Id) is null) 
            throw new NotFoundException(nameof(Order), orderDto.Id);
     
        if (await UnitOfWork.UserRepository.GetById(orderDto.UserId) is null)
            throw new NotFoundException(nameof(User), orderDto.UserId);

        await UnitOfWork.OrderRepository.Update(Mapper.Map<Order>(orderDto));
        await UnitOfWork.SaveChangesAsync();

        return orderDto;
    }


    public async Task Delete(int id)
    {
        var order = await UnitOfWork.OrderRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Order), id);

        await UnitOfWork.OrderRepository.Delete(order);
        await UnitOfWork.SaveChangesAsync();
    }

}