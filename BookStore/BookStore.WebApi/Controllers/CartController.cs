using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Cart;
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

    [HttpGet]
    public async Task<ActionResult<ICollection<CartDto>>> Get()
    {
        return Ok(await _cartService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartDto>> GetById(int id)
    {
        return Ok(await _cartService.GetById(id));
    }

    [HttpGet("{id}/items")]
    public async Task<ActionResult<CartItemDto>> GetAllItems(int id)
    {
        ISpecification<CartItem> specification = new CartItemsSpecification(id);
        return Ok(await _cartService.GetAllCartItems(specification));
    }

    [HttpPost]
    public async Task<ActionResult<CartDto>> Create([FromBody] NewCartDto cart)
    {
        return Ok(await _cartService.Create(cart));
    }

    [HttpPost("{cartId}/items/")]
    public async Task<IActionResult> AddItem(int cartId, [FromBody] NewCartItemDto cartItem)
    {
        return Ok(await _cartService.AddCartItem(cartId, cartItem));
    }
    
    [HttpPut("{cartId}/items/{cartItemId}")]
    public async Task<IActionResult> UpdateItem(int cartId, int cartItemId, [FromBody] NewCartItemDto cartItem)
    {
        return Ok(await _cartService.UpdateCartItem(cartId, cartItemId, cartItem));
    }
    
    [HttpDelete("{cartId}/items/{cartItemId}")]
    public async Task<IActionResult> DeleteItem(int cartId, int cartItemId)
    {
        await _cartService.DeleteCartItem(cartId, cartItemId);
        return NoContent();
    }

    [HttpDelete("{id}/clear")]
    public async Task<IActionResult> Clear(int id)
    {
        await _cartService.ClearCart(id);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cartService.Delete(id);
        return NoContent();
    }
}