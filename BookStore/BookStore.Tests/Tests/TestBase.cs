using AutoMapper;
using BookStore.BLL.MappingProfiles;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;
using BookStore.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class TestBase
{
    protected IUnitOfWork _unitOfWork;
    protected TestDbContext _context;
    protected Mapper _mapper;
    
    [SetUp]
    public virtual void SetUp()
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        builder.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        builder.EnableSensitiveDataLogging();
        
        _context = new TestDbContext(builder.Options);
        _unitOfWork = new UnitOfWork(_context);
        
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AuthorProfile>();
            cfg.AddProfile<BookProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<PublisherProfile>();
            cfg.AddProfile<ReviewProfile>();
            cfg.AddProfile<CartProfile>();
            cfg.AddProfile<OrderProfile>();
            cfg.AddProfile<CategoryProfile>();
        });
        _mapper = new Mapper(configuration);
    }
    
    protected Publisher CreatePublisher(string name)
    {
        Publisher publisher = new()
        {
            Name = name
        };

        _unitOfWork.PublisherRepository.Add(publisher).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return publisher;
    }
    
    protected Author CreateAuthor(string name)
    {
        Author author = new()
        {
            FullName = name
        };

        _unitOfWork.AuthorRepository.Add(author).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return author;
        
    }
    
    protected Book CreateBook(string name, int authorId, int publisherId, float price = 0, DateTime? publishDate = default)
    {
        Book book = new()
        {
            Name = name,
            AuthorId = authorId,
            PublisherId = publisherId,
            Price = price,
            PublishDate = publishDate ?? DateTime.Now
        };
        
        _unitOfWork.BookRepository.Add(book).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return book;
    }

    protected Category CreateCategory(string name)
    {
        Category category = new Category()
        {
            Name = name
        };
        _unitOfWork.CategoryRepository.Add(category).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return category;
    }

    protected BookCategory JoinBookCategory(int bookId, int categoryId)
    {
        var joint = new BookCategory()
        {
            BookId = bookId,
            CategoryId = categoryId,
        };
        _unitOfWork.BookCategoryRepository.Add(joint).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return joint;
    }

    protected User CreateUser(string name, string email, string password)
    {
        var user = new User()
        {
            Name = name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Email = email
        };
        _unitOfWork.UserRepository.Add(user).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return user;
    }

    protected Review CreateReview(int bookId, float rating, string textReview="")
    {
        var review = new Review()
        {
            BookId = bookId,
            Rating = rating,
            TextReview = textReview 
        };
        _unitOfWork.ReviewRepository.Add(review).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return review;
    }

    protected Cart CreateCart(int userId)
    {
        var cart = new Cart()
        {
            UserId = userId
        };
        _unitOfWork.CartRepository.Add(cart).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return cart;
    }
    
    protected CartItem CreateCartItem(int cartId, Book book, int count)
    {
        var cartItem = new CartItem()
        {
            CartId = cartId,
            BookId = book.Id,
            Count = count,
            Price = book.Price*count
        };
        _unitOfWork.CartItemRepository.Add(cartItem).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return cartItem;
    }

    protected Order CreateOrder(int userId, string address = "", OrderState orderState = OrderState.Processing, DateTime? orderTime = default)
    {
        var order = new Order()
        {
            UserId = userId,
            Address = address,
            OrderState = orderState,
            OrderDateTime = orderTime ?? DateTime.Now
        };
        _unitOfWork.OrderRepository.Add(order).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return order;
    }
    
    protected OrderItem CreateOrderItem(int orderId, Book book, int count)
    {
        var orderItem = new OrderItem()
        {
            OrderId = orderId,
            BookId = book.Id,
            Count = count,
            Price = book.Price*count
        };
        _unitOfWork.OrderItemRepository.Add(orderItem).GetAwaiter().GetResult();
        _unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        return orderItem;
    }
}