using BookStore.BLL.DTOs.Order;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Orders;
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet]
    public async Task<ActionResult<ICollection<OrderDto>>> Get()
    {
        return Ok(await _orderService.GetAll());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _orderService.GetById(id);

        if (order.UserId != User.GetUserId() && !User.IsInRole(nameof(UserRole.Admin)))
            return Forbid();
        
        return Ok(order);
    }

    [Authorize]
    [HttpGet("{id}/items")]
    public async Task<ActionResult<ICollection<OrderItemDto>>> GetAllItems(int id)
    {
        var order = await _orderService.GetById(id);

        if (order.UserId != User.GetUserId() && !User.IsInRole(nameof(UserRole.Admin)))
            return Forbid();
        
        ISpecification<OrderItem> specification = new OrderItemsSpecification(id);
        return Ok(await _orderService.GetAllOrderItems(specification));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] NewOrderDto order)
    {
        order.UserId = User.GetUserId();
        return Ok(await _orderService.Create(order));
    }
    
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OrderDto orderItem)
    {
        return Ok(await _orderService.Update(orderItem));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.Delete(id);
        return NoContent();
    }
}