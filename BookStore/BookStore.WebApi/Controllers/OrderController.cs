using BookStore.BLL.DTOs.Order;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Orders;
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

    [HttpGet]
    public async Task<ActionResult<ICollection<OrderDto>>> Get()
    {
        return Ok(await _orderService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        return Ok(await _orderService.GetById(id));
    }

    [HttpGet("{id}/items")]
    public async Task<ActionResult<OrderItemDto>> GetAllItems(int id)
    {
        ISpecification<OrderItem> specification = new OrderItemsSpecification(id);
        return Ok(await _orderService.GetAllOrderItems(specification));
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] NewOrderDto order)
    {
        return Ok(await _orderService.Create(order));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OrderDto orderItem)
    {
        return Ok(await _orderService.Update(orderItem));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.Delete(id);
        return NoContent();
    }
}