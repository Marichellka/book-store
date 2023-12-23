using AutoMapper;
using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
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

    public async Task<ICollection<CartItemDto>> GetAllCartItems(ISpecification<CartItem>? specification= null)
    {
        var items = await UnitOfWork.CartItemRepository.GetAll(specification);

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

    public async Task<CartItemDto> AddCartItem(int cartId, NewCartItemDto newCartItem)
    {
        var cart = await UnitOfWork.CartRepository.GetById(cartId) ?? 
                   throw new NotFoundException(nameof(Cart), cartId);
        
        var book = await UnitOfWork.BookRepository.GetById(newCartItem.BookId) ?? 
                   throw new NotFoundException(nameof(Book), newCartItem.BookId);

        var cartItem = Mapper.Map<CartItem>(newCartItem);
        cartItem.CartId = cartId;
        cartItem.Price = book.Price * newCartItem.Count;

        cart.TotalPrice += cartItem.Price;
        
        await UnitOfWork.CartItemRepository.Add(cartItem);
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<CartItemDto>(cartItem);
    }
    
    public async Task<CartItemDto> UpdateCartItem(int cartId, int cartItemId, CartItemDto cartItemDto)
    {
        var book = await UnitOfWork.BookRepository.GetById(cartItemDto.BookId) ??
            throw new NotFoundException(nameof(Book), cartItemDto.BookId);

        var cart = await UnitOfWork.CartRepository.GetById(cartId) ?? 
                   throw new NotFoundException(nameof(Cart), cartId);
        
        var cartItem = await UnitOfWork.CartItemRepository.GetById(cartItemId) ??
                       throw new NotFoundException(nameof(CartItem), cartItemId);

        cart.TotalPrice -= cartItem.Price;
        cart.TotalPrice += cartItemDto.Count*book.Price;
        
        await UnitOfWork.CartItemRepository.Update(Mapper.Map<CartItem>(cartItemDto));
        await UnitOfWork.CartRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<CartItemDto>(cartItem);
    }

    public async Task DeleteCartItem(int cartId, int cartItemId)
    {
        var cart = await UnitOfWork.CartRepository.GetById(cartId) ?? 
                   throw new NotFoundException(nameof(Cart), cartId);

        var item = await UnitOfWork.CartItemRepository.GetById(cartItemId) ??
                   throw new NotFoundException(nameof(cartItemId), cartItemId);
        
        if (await UnitOfWork.BookRepository.GetById(item.BookId) is null)
            throw new NotFoundException(nameof(Book), item.BookId);
        
        cart.TotalPrice -= item.Price;
        
        await UnitOfWork.CartItemRepository.Delete(Mapper.Map<CartItem>(item));
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
    
    public async Task Delete(int id)
    {
        var cart = await UnitOfWork.CartRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Cart), id);

        await UnitOfWork.CartRepository.Delete(cart);
        await UnitOfWork.SaveChangesAsync();
    }
}