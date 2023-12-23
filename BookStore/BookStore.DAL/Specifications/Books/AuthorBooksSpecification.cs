using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Books;

public class AuthorBooksSpecification: ISpecification<Book>
{
    private readonly int _authorId;

    public AuthorBooksSpecification(int authorId)
    {
        _authorId = authorId;
    }

    public Expression<Func<Book, bool>> SpecificationExpression => book => book.AuthorId == _authorId; 
}