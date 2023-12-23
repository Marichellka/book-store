using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Reviews;

public class BookReviewSpecification: ISpecification<Review>
{
    private readonly int _bookId;

    public BookReviewSpecification(int bookId)
    {
        _bookId = bookId;
    }
    public Expression<Func<Review, bool>> SpecificationExpression => review => review.BookId == _bookId;
}