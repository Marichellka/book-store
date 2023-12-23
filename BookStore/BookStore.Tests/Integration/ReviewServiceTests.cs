using BookStore.BLL.DTOs.Review;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Services;
using BookStore.DAL.Specifications.Reviews;

namespace BookStore.Tests.Integration;

public class ReviewServiceTests: ServiceTestBase
{
    private ReviewService _reviewService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _reviewService = new ReviewService(_unitOfWork, _mapper);
    }
    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _reviewService.GetAll();

        Assert.IsEmpty(returnedList);
    }
    
    [Test]
    public async Task GetAll_BooksReviews_ReturnedFilteredList()
    {
        var book1 = CreateBook("Book1", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var book2 = CreateBook("Book2", CreateAuthor("author").Id, CreatePublisher("publisher").Id);

        var review = CreateReview(book1.Id, 3.5f);
        CreateReview(book2.Id, 3.5f);

        var expectedList = new List<ReviewDto>()
        {
            _mapper.Map<ReviewDto>(review)
        };

        var returnedList = await _reviewService.GetAll(new BookReviewSpecification(book1.Id));

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }

    [Test]
    public void GetById_ReviewDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _reviewService.GetById(42));
    }

    [Test]
    public async Task GetById_ReviewExists_ReturnsReview()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var review = CreateReview(book.Id, 3.5f);

        var expected = _mapper.Map<ReviewDto>(review);

        var returned = await _reviewService.GetById(review.Id);
        
        Assert.That(returned, Is.EqualTo(expected));
    }
    
    [Test]
    public void Update_ReviewDoesNotExist_ThrownNotFoundException()
    {
        ReviewDto categoryDto = new ReviewDto()
        {
            Id = 1,
        };

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _reviewService.Update(categoryDto));
    }
    
    [Test]
    public async Task Update_ReviewExists_ReviewUpdated()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var review = CreateReview(book.Id, 3.5f);


        ReviewDto updated = new ReviewDto()
        {
            Id = review.Id,
            Rating = 5,
            TextReview = "great"
        };

        var expected = new ReviewDto()
        {
            Id = review.Id,
            Rating = 5,
            TextReview = "great"
        };

        var returnedCategory = await _reviewService.Update(updated);

        Assert.That(returnedCategory, Is.EqualTo(expected));
    }

    [Test]
    public void Delete_ReviewDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _reviewService.Delete(45));
    }

    [Test]
    public async Task Delete_ReviewExists_ReviewDeleted()
    {
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);
        var review = CreateReview(book.Id, 3.5f);

        await _reviewService.Delete(review.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _reviewService.GetById(review.Id));
    }
    
    [Test]
    public void Create_UserDoesNotExist_ThrowsNotFound()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await _reviewService.Create(new() { UserId = 42 }));
    }
    
    [Test]
    public void Create_BookDoesNotExist_ThrowsNotFound()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        Assert.ThrowsAsync<NotFoundException>(async () => await _reviewService.Create(new() { UserId = user.Id, BookId = 42}));
    }
    
    [Test]
    public async Task Create_DataCorrect_BookCreated()
    {
        var user = CreateUser("User", "some@mail.com", "Password");
        var book = CreateBook("Book", CreateAuthor("author").Id, CreatePublisher("publisher").Id);

        await _reviewService.Create(new NewReviewDto() {UserId = user.Id, BookId = book.Id, Rating = 5});
        
        Assert.That(_context.Reviews.Count(), Is.EqualTo(1));
    }
    
}