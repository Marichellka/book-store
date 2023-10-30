using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Review;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class ReviewService: BaseService
{
    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<ReviewDto>> GetAll()
    {
        var reviews = await UnitOfWork.ReviewRepository.GetAll();
        return Mapper.Map<ICollection<ReviewDto>>(reviews);
    }

    public async Task<ICollection<BookDto>> GetAllBooksReview(int bookId)
    {
        var reviews = await UnitOfWork.ReviewRepository.GetAll(review => review.BookId == bookId);

        return Mapper.Map<ICollection<BookDto>>(reviews);
    }
    
    public async Task<ICollection<BookDto>> GetAllUsersReview(int userId)
    {
        var reviews = await UnitOfWork.ReviewRepository.GetAll(review => review.UserId == userId);

        return Mapper.Map<ICollection<BookDto>>(reviews);
    }

    public async Task<ReviewDto> GetById(int id)
    {
        var review = await UnitOfWork.ReviewRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Review), id);

        return Mapper.Map<ReviewDto>(review);
    }
    
    public async Task<ReviewDto> Create(NewReviewDto review)
    {
        if(await UnitOfWork.UserRepository.GetById(review.UserId) is null) 
            throw new NotFoundException(nameof(User), review.UserId);
        
        if(await UnitOfWork.BookRepository.GetById(review.BookId) is null) 
            throw new NotFoundException(nameof(Book), review.BookId);
        
        var reviewEntity = Mapper.Map<Review>(review);
        await UnitOfWork.ReviewRepository.Add(reviewEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<ReviewDto>(reviewEntity);
    }

    public async Task<ReviewDto> Update(ReviewDto updatedReview)
    {
        if (await UnitOfWork.ReviewRepository.GetById(updatedReview.Id) is null)
            throw new NotFoundException(nameof(Review), updatedReview.Id);

        await UnitOfWork.ReviewRepository.Update(Mapper.Map<Review>(updatedReview));
        await UnitOfWork.SaveChangesAsync();

        return updatedReview;
    }

    public async Task Delete(int id)
    {
        var review = await UnitOfWork.ReviewRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Review), id);

        await UnitOfWork.ReviewRepository.Delete(review);
        await UnitOfWork.SaveChangesAsync();
    }
}