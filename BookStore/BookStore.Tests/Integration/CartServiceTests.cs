using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Services;
using BookStore.DAL.Specifications.Cart;

namespace BookStore.Tests.Integration;

public class CartServiceTests: ServiceTestBase
{
    private  CartService _cartService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _cartService = new CartService(_unitOfWork, _mapper);
    }
    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _cartService.GetAll();
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);

        var expectedList = new List<CartDto>() { _mapper.Map<CartDto>(cart) };

        var returnedList =  await _cartService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public async Task GetAllCartItems_NoItems_ReturnedEmptyList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        
        var returnedList = await _cartService.GetAllCartItems(new CartItemsSpecification(cart.Id));
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAllCartItems_HasItems_ReturnedList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        var item = CreateCartItem(cart.Id, book, 2);

        var expectedList = new List<CartItemDto>() { _mapper.Map<CartItemDto>(item) };

        var returnedList =  await _cartService.GetAllCartItems(new CartItemsSpecification(cart.Id));

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public void GetById_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _cartService.GetById(42));
    }

    [Test]
    public async Task GetById_BookExists_ReturnsBook()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);

        CartDto expected = _mapper.Map<CartDto>(cart);

        CartDto result = await _cartService.GetById(cart.Id);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void AddCartItem_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.AddCartItem(42, new NewCartItemDto()));
    }
    
    [Test]
    public void AddCartItem_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.AddCartItem(42, new NewCartItemDto(){BookId=1}));
    }

    [Test]
    public async Task AddCartItem_BookExists_ItemAdded()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);

        NewCartItemDto item = new NewCartItemDto()
        {
            BookId = book.Id,
            Count = 2
        };
        
        await _cartService.AddCartItem(cart.Id, item);
        var returned = await _cartService.GetAllCartItems(new CartItemsSpecification(cart.Id));
        
        Assert.That(returned, Has.Count.EqualTo(1));
    }
    
    [Test]
    public void UpdateCartItem_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.UpdateCartItem(42, 42, new CartItemDto()));
    }
    
    [Test]
    public void UpdateCartItem_CartItemDoesNotExist_ThrowsNotFound()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.UpdateCartItem(cart.Id, 42, new CartItemDto()));
    }
    
    [Test]
    public async Task UpdateCartItem_CartItemExists_CartItemUpdated()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        var item = CreateCartItem(cart.Id, book, 2);

        var updated = new CartItemDto()
        {
            Id = item.Id,
            BookId = book.Id,
            CartId = cart.Id,
            Count = 3,
        };

        var expected = new CartItemDto()
        {
            Id = item.Id,
            BookId = book.Id,
            CartId = cart.Id,
            Count = 3,
            Price = 3 * book.Price
        };

        var returned = await _cartService.UpdateCartItem(cart.Id, item.Id, updated);
        
        Assert.That(returned, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteCartItem_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.DeleteCartItem(42, 42));
    }
    
    [Test]
    public void DeleteCartItem_CartItemDoesNotExist_ThrowsNotFound()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.DeleteCartItem(cart.Id, 42));
    }

    [Test]
    public async Task DeleteCartItem_ItemExists_ItemAdded()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        var item = CreateCartItem(cart.Id, book, 2);
        
        await _cartService.DeleteCartItem(cart.Id, item.Id);
        var returned = await _cartService.GetAllCartItems(new CartItemsSpecification(cart.Id));
        
        Assert.That(returned, Is.Empty);
    }
    
    
    [Test]
    public void ClearCart_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.ClearCart(42));
    }
    
    [Test]
    public async Task ClearCart_CartExists_CartCleared()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id, 50);
        CreateCartItem(cart.Id, book, 2);
        
        await _cartService.ClearCart(cart.Id);
        var returned = await _cartService.GetAllCartItems(new CartItemsSpecification(cart.Id));
        
        Assert.That(returned, Is.Empty);
    }
    
    [Test]
    public void Delete_CartDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _cartService.Delete(42));
    }
    
    [Test]
    public async Task Delete_CartExists_CartDeleted()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var cart = CreateCart(user.Id);

        await _cartService.Delete(cart.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _cartService.GetById(cart.Id));
    }
}