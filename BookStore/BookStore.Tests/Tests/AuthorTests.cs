using BookStore.BLL.DTOs.Author;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Services;

namespace BookStore.Tests.Tests;

public class AuthorTests: TestBase
{
    private  AuthorService _authorService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _authorService = new AuthorService(_unitOfWork, _mapper);
    }
    
    [Test]
    public async Task GetBooks_NoBooks_ReturnedEmptyList()
    {
        var author = CreateAuthor("Author");
        
        var returnedList = await _authorService.GetBooks(author.Id);
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public void GetBooks_NonExistingAuthor_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task () => await _authorService.GetBooks(1));
    }
    
    [Test]
    public async Task GetBooks_WithBooks_ReturnedList()
    {
        var author = CreateAuthor("Author");
        var publisher = CreatePublisher("Publisher");
        var book1 = CreateBook("Book1", author.Id, publisher.Id);
        var book2 = CreateBook("Book2", author.Id, publisher.Id);

        List<BookDto> expectedList = new List<BookDto>()
        {
            _mapper.Map<BookDto>(book1),
            _mapper.Map<BookDto>(book2),
        };

        
        var returnedList = await _authorService.GetBooks(author.Id);
        
        Assert.That(expectedList, Is.EqualTo(returnedList));
    }

    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _authorService.GetAll();
        
        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var author = CreateAuthor("Author");

        var expectedList = new List<AuthorDto>()
        {
            _mapper.Map<AuthorDto>(author)
        };
        
        var returnedList = await _authorService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public void GetById_NonExistingAuthor_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task () => await _authorService.GetById(1));
    }
    
    [Test]
    public async Task GetById_ExistingAuthor_ReturnedAuthor()
    {
        var author = CreateAuthor("Author");

        var expectedAuthor = _mapper.Map<AuthorDto>(author);
        
        var returnedAuthor = await _authorService.GetById(author.Id);
        
        Assert.That(returnedAuthor, Is.EqualTo(expectedAuthor));
    }
    
    [Test]
    public void Update_NonExistingAuthor_ThrownNotFoundException()
    {
        AuthorDto authorDto = new AuthorDto()
        {
            Id = 1,
            FullName = "Author"
        };
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task () => await _authorService.Update(authorDto));
    }
    
    [Test]
    public async Task Update_ExistingAuthor_AuthorUpdated()
    {
        var author = CreateAuthor("Author");
        
        AuthorDto updatedAuthor = new AuthorDto()
        {
            Id = author.Id,
            FullName = "Author2"
        };
        
        var expectedAuthor = new AuthorDto()
        {
            Id = author.Id,
            FullName = "Author2",
        };
        
        var returnedAuthor = await _authorService.Update(updatedAuthor);
        
        Assert.That(returnedAuthor, Is.EqualTo(expectedAuthor));
    }
    
    [Test]
    public void Delete_NonExistingAuthor_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task () => await _authorService.Delete(1));
    }
    
    [Test]
    public async Task Delete_ExistingAuthor_AuthorDeleted()
    {
        var author = CreateAuthor("Author");

        await _authorService.Delete(author.Id);
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task () => await _authorService.GetById(author.Id));
    }
}