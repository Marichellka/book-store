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
    
    // private ReviewDto CreateReview(int userId, int bookId, float rating = 0, string text = "")
    // {
    //     NewReviewDto review = new()
    //     {
    //         UserId = userId,
    //         BookId = bookId,
    //         Rating = rating,
    //         TextReview = text
    //     };
    //     
    //     
    // }
}