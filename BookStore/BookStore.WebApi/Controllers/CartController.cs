using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Cart;
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController: ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet]
    public async Task<ActionResult<ICollection<CartDto>>> Get()
    {
        return Ok(await _cartService.GetAll());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<CartDto>> GetById(int id)
    {
        var cart = await _cartService.GetById(id);

        if (cart.UserId != User.GetUserId())
            return Forbid();
            
        return Ok(cart);
    }

    [Authorize]
    [HttpGet("{id}/items")]
    public async Task<ActionResult<CartItemDto>> GetAllItems(int id)
    {
        var cart = await _cartService.GetById(id);

        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        ISpecification<CartItem> specification = new CartItemsSpecification(id);
        return Ok(await _cartService.GetAllCartItems(specification));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CartDto>> Create([FromBody] NewCartDto cart)
    {
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        return Ok(await _cartService.Create(cart));
    }

    [Authorize]
    [HttpPost("{cartId}/items/")]
    public async Task<IActionResult> AddItem(int cartId, [FromBody] NewCartItemDto cartItem)
    {
        var cart = await _cartService.GetById(cartId);
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        return Ok(await _cartService.AddCartItem(cartId, cartItem));
    }
    
    [Authorize]
    [HttpPut("{cartId}/items/{cartItemId}")]
    public async Task<IActionResult> UpdateItem(int cartId, int cartItemId, [FromBody] CartItemDto cartItem)
    {
        var cart = await _cartService.GetById(cartId);
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        return Ok(await _cartService.UpdateCartItem(cartId, cartItemId, cartItem));
    }
    
    [Authorize]
    [HttpDelete("{cartId}/items/{cartItemId}")]
    public async Task<IActionResult> DeleteItem(int cartId, int cartItemId)
    {
        var cart = await _cartService.GetById(cartId);
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        await _cartService.DeleteCartItem(cartId, cartItemId);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}/clear")]
    public async Task<IActionResult> Clear(int id)
    {
        var cart = await _cartService.GetById(id);
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        await _cartService.ClearCart(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cart = await _cartService.GetById(id);
        if (cart.UserId != User.GetUserId())
            return Forbid();
        
        await _cartService.Delete(id);
        return NoContent();
    }
}