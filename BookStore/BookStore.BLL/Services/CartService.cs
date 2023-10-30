using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class CartService: BaseService
{
    public CartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<CartDto>> GetAll()
    {
        var carts = await UnitOfWork.CartRepository.GetAll();
        return Mapper.Map<ICollection<CartDto>>(carts);
    }

    public async Task<ICollection<CartItemDto>> GetAllCartItems(int cartId)
    {
        var items = await UnitOfWork.CartItemRepository.GetAll(item => item.CartId == cartId);

        return Mapper.Map<ICollection<CartItemDto>>(items);
    }

    public async Task<CartDto> GetById(int id)
    {
        var cart = await UnitOfWork.CartRepository.GetById(id) ??
                        throw new NotFoundException(nameof(Cart), id);

        return Mapper.Map<CartDto>(cart);
    }
    
    public async Task<CartDto> Create(NewCartDto cart)
    {
        if (await UnitOfWork.UserRepository.GetById(cart.UserId) is null)
            throw new NotFoundException(nameof(User), cart.UserId);
        
        var cartEntity = Mapper.Map<Cart>(cart);
        await UnitOfWork.CartRepository.Add(cartEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<CartDto>(cartEntity);
    }

    public async Task<CartItemDto> AddCartItem(NewCartItemDto newCartItem)
    {
        var cart = await UnitOfWork.CartRepository.GetById(newCartItem.CartId) ?? 
                   throw new NotFoundException(nameof(Cart), newCartItem.CartId);
        
        var book = await UnitOfWork.BookRepository.GetById(newCartItem.BookId) ?? 
                   throw new NotFoundException(nameof(Book), newCartItem.BookId);

        var cartItem = Mapper.Map<CartItem>(newCartItem);
        cartItem.Price = book.Price * newCartItem.Count;

        cart.TotalPrice += cartItem.Price;
        
        await UnitOfWork.CartItemRepository.Add(cartItem);
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<CartItemDto>(cartItem);
    }
    
    public async Task<CartItemDto> UpdateCartItem(CartItemDto cartItemDto)
    {
        if (await UnitOfWork.BookRepository.GetById(cartItemDto.BookId) is null)
            throw new NotFoundException(nameof(Book), cartItemDto.BookId);

        var cart = await UnitOfWork.CartRepository.GetById(cartItemDto.CartId) ?? 
                   throw new NotFoundException(nameof(Cart), cartItemDto.CartId);
        
        var cartItem = await UnitOfWork.CartItemRepository.GetById(cartItemDto.Id) ??
                       throw new NotFoundException(nameof(CartItem), cartItemDto.Id);

        cart.TotalPrice -= cartItem.Price;
        cart.TotalPrice += cartItemDto.Price;
        
        await UnitOfWork.CartItemRepository.Update(Mapper.Map<CartItem>(cartItemDto));
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();

        return cartItemDto;
    }

    public async Task DeleteCartItem(CartItemDto cartItemDto)
    {
        var cart = await UnitOfWork.CartRepository.GetById(cartItemDto.CartId) ?? 
                   throw new NotFoundException(nameof(Cart), cartItemDto.CartId);
        
        if (await UnitOfWork.BookRepository.GetById(cartItemDto.BookId) is null)
            throw new NotFoundException(nameof(Book), cartItemDto.BookId);
        
        cart.TotalPrice -= cartItemDto.Price;
        
        await UnitOfWork.CartItemRepository.Delete(Mapper.Map<CartItem>(cartItemDto));
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();
    }
    
    public async Task ClearCart(int id)
    {
        var cart = await UnitOfWork.CartRepository.GetById(id) ??
                        throw new NotFoundException(nameof(Cart), id);

        cart.CartItems = null;
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();
    }
}