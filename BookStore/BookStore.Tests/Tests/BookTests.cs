using AutoMapper;
using BookStore.BLL.DTOs.Author;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Category;
using BookStore.BLL.DTOs.Publisher;
using BookStore.BLL.Exceptions;
using BookStore.BLL.MappingProfiles;
using BookStore.BLL.Services;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Books;
using BookStore.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookStore.Tests.Tests;

public class BookTests: TestBase
{
    private  BookService _bookService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _bookService = new BookService(_unitOfWork, _mapper);
    }

    [Test]
    public async Task GetBooks_EmptyDB_ReturnsEmpty()
    {
        var books = await _bookService.GetAll();

        Assert.That(books, Is.Empty);
    }

    [Test]
    public async Task GetBooks_WithSpecifications_ReturnsFilteredBooks()
    {
        var publisher1 = CreatePublisher("publisher1");
        var publisher2 = CreatePublisher("publisher2");
        var author1 = CreateAuthor("author1");
        var author2 = CreateAuthor("author2");
        var category1 = CreateCategory("category1");
        var category2 = CreateCategory("category2");

        var book1 = CreateBook("1", author1.Id, publisher1.Id);
        var book2 = CreateBook("2", author2.Id, publisher1.Id);
        var book3 = CreateBook("3", author2.Id, publisher2.Id);
        var book4 = CreateBook("4", author1.Id, publisher1.Id);
        JoinBookCategory(book1.Id, category1.Id);
        JoinBookCategory(book2.Id, category2.Id);
        JoinBookCategory(book3.Id, category1.Id);
        JoinBookCategory(book4.Id, category2.Id);

        var expected = new List<BookDto>()
        {
            _mapper.Map<BookDto>(book1)
        };
        
        var books = await _bookService.GetAll(
            new AndSpecification<Book>(
                new PublisherBooksSpecification(publisher1.Id), 
                new AndSpecification<Book>(
                    new AuthorBooksSpecification(author1.Id),
                    new CategoryBooksSpecification(category1.Id))));
        
        Assert.That(books, Is.EqualTo(expected));
    }

    [Test]
    public void GetById_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _bookService.GetById(42));
    }

    [Test]
    public async Task GetById_BookExists_ReturnsBook()
    {
        var publisher = CreatePublisher("publisher");
        var author = CreateAuthor("author");

        var book = CreateBook("book", author.Id, publisher.Id);

        BookDto expected = _mapper.Map<BookDto>(book);

        BookDto result = await _bookService.GetById(book.Id);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Update_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _bookService.Update(new() { Id = 42 }));
    }
    
    [Test]
    public async Task Update_BookExists_BookUpdated()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);

        BookDto updatedBook = new BookDto()
        {
            Id = book.Id,
            Name = "Book2",
            Price = 2.1f,
        };

        var expectedBook = new BookDto()
        {
            Id = book.Id,
            Name = "Book2",
            Price = 2.1f,
        };

        var returnedBook = await _bookService.Update(updatedBook);

        Assert.That(returnedBook, Is.EqualTo(expectedBook));
    }
    
    [Test]
    public void Delete_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _bookService.Delete(42));
    }
    
    [Test]
    public async Task Delete_BookExists_BookDeleted()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);

        await _bookService.Delete(book.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.GetById(book.Id));
    }
    
    [Test]
    public void GetCategories_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.GetCategories(42));
    }
    
    [Test]
    public async Task GetCategories_BookExists_ReturnedList()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var category1 = CreateCategory("Category1");
        var category2 = CreateCategory("Category2");
        JoinBookCategory(book.Id, category1.Id);

        var expected = new List<CategoryDto>()
        {
            _mapper.Map<CategoryDto>(category1)
        };

        var returned = await _bookService.GetCategories(book.Id);
        
        Assert.That(returned, Is.EqualTo(expected));
    }

    
    [Test]
    public void AddCategory_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.AddCategory(42, new CategoryDto(){Id=1}));
    }
    
    [Test]
    public void AddCategory_CategoryDoesNotExist_ThrowsNotFound()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.AddCategory(book.Id, new CategoryDto(){Id=1}));
    }
    
    [Test]
    public async Task AddCategory_CategoryExists_CategoryAdded()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var category = CreateCategory("Category");
        
        var expected = new List<CategoryDto>()
        {
            _mapper.Map<CategoryDto>(category)
        };
        
        await _bookService.AddCategory(book.Id, _mapper.Map<CategoryDto>(category));
        var returned = await _bookService.GetCategories(book.Id);
        
        Assert.That(returned, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteCategory_BookDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.DeleteCategory(42, 42));
    }
    
    [Test]
    public void DeleteCategory_CategoryDoesNotExist_ThrowsNotFound()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _bookService.DeleteCategory(book.Id, 42));
    }
    
    [Test]
    public async Task DeleteCategory_CategoryExists_CategoryAdded()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var category = CreateCategory("Category");
        JoinBookCategory(book.Id, category.Id);
        
        await _bookService.DeleteCategory(book.Id, category.Id);
        var returned = await _bookService.GetCategories(book.Id);
        
        Assert.That(returned, Is.Empty);
    }
    
    [Test]
    public void Create_AuthorDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _bookService.Create(new() { AuthorId = 42 }));
    }
    
    [Test]
    public void Create_PublisherDoesNotExist_ThrowsNotFound()
    {
        var publisher = CreatePublisher("publisher");
        var author = CreateAuthor("author");
        Assert.ThrowsAsync<NotFoundException>(async () => await _bookService.Create(new() { AuthorId = author.Id, PublisherId = 42}));
    }
    
    [Test]
    public async Task Create_AllExist_BookCreated()
    {
        var publisher = CreatePublisher("publisher");
        var author = CreateAuthor("author");

        await _bookService.Create(new NewBookDto() { AuthorId = author.Id, PublisherId = publisher.Id, Name = "Book"});
        
        Assert.That(_context.Books.Count(), Is.EqualTo(1));
    }
}
