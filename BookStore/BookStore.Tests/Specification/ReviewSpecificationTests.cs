using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Reviews;

namespace BookStore.Tests.Specification;

public class ReviewSpecificationTests : SpecificationTestBase
{
    [Test]
    public void BookReviewSpecification_Satisfies()
    {
        BookReviewSpecification specification = new(bookId: 1);

        bool result = IsSatisfiedBy(specification, new Review { BookId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void BookReviewSpecification_DoesNotSatisfy()
    {
        BookReviewSpecification specification = new(bookId: 1);

        bool result = IsSatisfiedBy(specification, new Review { BookId = 2 });

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserReviewSpecification_Satisfies()
    {
        UserReviewSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Review { UserId = 1 });

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserReviewSpecification_DoesNotSatisfy()
    {
        UserReviewSpecification specification = new(userId: 1);

        bool result = IsSatisfiedBy(specification, new Review { UserId = 2 });

        Assert.That(result, Is.False);
    }
}
