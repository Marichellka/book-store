using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Publisher;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Services;

namespace BookStore.Tests.Integration;

public class PublisherServiceTests: ServiceTestBase
{
    private PublisherService _publisherService;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _publisherService = new PublisherService(_unitOfWork, _mapper);
    }

    [Test]
    public async Task GetBooks_NoBooks_ReturnedEmptyList()
    {
        var publisher = CreatePublisher("Publisher");

        var returnedList = await _publisherService.GetBooks(publisher.Id);

        Assert.IsEmpty(returnedList);
    }

    [Test]
    public void GetBooks_PublisherDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _publisherService.GetBooks(1));
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

        var returnedList = await _publisherService.GetBooks(publisher.Id);

        Assert.That(expectedList, Is.EqualTo(returnedList));
    }


    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _publisherService.GetAll();

        Assert.IsEmpty(returnedList);
    }

    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var publisher = CreatePublisher("Publisher");

        var expectedList = new List<PublisherDto>()
        {
            _mapper.Map<PublisherDto>(publisher)
        };

        var returnedList = await _publisherService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }

    [Test]
    public void GetById_PublisherDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _publisherService.GetById(1));
    }

    [Test]
    public async Task GetById_PublisherExists_ReturnedPublisher()
    {
        var publisher = CreatePublisher("Publisher");

        var expectedPublisher = _mapper.Map<PublisherDto>(publisher);

        var returnedPublisher = await _publisherService.GetById(publisher.Id);

        Assert.That(returnedPublisher, Is.EqualTo(expectedPublisher));
    }

    [Test]
    public void Update_PublisherDoesNotExist_ThrownNotFoundException()
    {
        PublisherDto publisherDto = new PublisherDto()
        {
            Id = 1,
            Name = "Publisher"
        };

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _publisherService.Update(publisherDto));
    }

    [Test]
    public async Task Update_PublisherExists_PublisherUpdated()
    {
        var publisher = CreatePublisher("Publisher");

        PublisherDto updatedPublisher = new PublisherDto()
        {
            Id = publisher.Id,
            Name = "Publisher2"
        };

        var expectedPublisher = new PublisherDto()
        {
            Id = publisher.Id,
            Name = "Publisher2",
        };

        var returnedPublisher = await _publisherService.Update(updatedPublisher);

        Assert.That(returnedPublisher, Is.EqualTo(expectedPublisher));
    }

    [Test]
    public void Delete_PublisherDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _publisherService.Delete(1));
    }

    [Test]
    public async Task Delete_PublisherExists_PublisherDeleted()
    {
        var publisher = CreatePublisher("Publisher");

        await _publisherService.Delete(publisher.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _publisherService.GetById(publisher.Id));
    }
}