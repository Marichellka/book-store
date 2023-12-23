using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Books;

public class PublisherBooksSpecification: ISpecification<Book>
{
    private readonly int _publisherId;

    public PublisherBooksSpecification(int publisherId)
    {
        _publisherId = publisherId;
    }

    public Expression<Func<Book, bool>> SpecificationExpression => book => book.PublisherId == _publisherId;
}