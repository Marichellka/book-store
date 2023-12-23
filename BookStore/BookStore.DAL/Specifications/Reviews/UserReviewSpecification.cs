using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Reviews;

public class UserReviewSpecification: ISpecification<Review>
{
    private readonly int _userId;

    public UserReviewSpecification(int userId)
    {
        _userId = userId;
    }
    public Expression<Func<Review, bool>> SpecificationExpression => review => review.UserId == _userId;
}