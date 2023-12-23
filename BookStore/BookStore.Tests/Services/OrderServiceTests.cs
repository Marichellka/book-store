using BookStore.BLL.DTOs.Order;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Orders;

namespace BookStore.Tests.Services;

public class OrderServiceTests: ServiceTestBase
{
    private  OrderService _orderService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _orderService = new OrderService(_unitOfWork, _mapper);
    }
    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _orderService.GetAll();
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);

        var expectedList = new List<OrderDto>() { _mapper.Map<OrderDto>(order) };

        var returnedList =  await _orderService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public async Task GetAllOrderItems_NoItems_ReturnedEmptyList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);
        
        var returnedList = await _orderService.GetAllOrderItems(new OrderItemsSpecification(order.Id));
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAllOrderItems_HasItems_ReturnedList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        var item = CreateOrderItem(order.Id, book, 2);

        var expectedList = new List<OrderItemDto>() { _mapper.Map<OrderItemDto>(item) };

        var returnedList =  await _orderService.GetAllOrderItems(new OrderItemsSpecification(order.Id));

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public void GetById_OrderDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.GetById(42));
    }

    [Test]
    public async Task GetById_BookExists_ReturnsBook()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);

        OrderDto expected = _mapper.Map<OrderDto>(order);

        OrderDto result = await _orderService.GetById(order.Id);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void Update_OrderDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.Update(new() { Id = 42 }));
    }
    
    [Test]
    public async Task Update_OrderExists_BookUpdated()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);

        OrderDto updated = new OrderDto()
        {
            Id = order.Id,
            UserId = user.Id,
            OrderState = OrderState.Delivering
        };

        var expected = _mapper.Map<OrderDto>(order);
        order.OrderState = OrderState.Delivering;

        var returned = await _orderService.Update(updated);

        Assert.That(returned, Is.EqualTo(expected));
    }
    
    [Test]
    public void Delete_OrderDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _orderService.Delete(42));
    }
    
    [Test]
    public async Task Delete_OrderExists_CartDeleted()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var order = CreateOrder(user.Id);

        await _orderService.Delete(order.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _orderService.GetById(order.Id));
    }
    
    [Test]
    public void CreateOrder_UserDoesNotExist_ThrowsNotFound()
    {
        var order = new NewOrderDto()
        {
            UserId = 42
        };
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _orderService.Create(order));
    }
    
    [Test]
    public void CreateOrder_CartDoesNotExist_ThrowsNotFound()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        var order = new NewOrderDto()
        {
            UserId = user.Id
        };
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _orderService.Create(order));
    }
    
    [Test]
    public async Task CreateOrder_CartExists_OrderCreated()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        CreateCartItem(cart.Id, book, 2);

        var newOrder = new NewOrderDto()
        {
            UserId = user.Id,
            Address = "some address"
        };

        var order = await _orderService.Create(newOrder);
        
        Assert.That(_context.Orders.Count(), Is.EqualTo(1));
    }
}