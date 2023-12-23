using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Books;

public class BookCategoriesSpecification: ISpecification<Category>
{
    private readonly int _bookId;

    public BookCategoriesSpecification(int bookId)
    {
        _bookId = bookId;
    }


    public Expression<Func<Category, bool>> SpecificationExpression => category => 
        category.BookCategories != null && category.BookCategories.Any(bookCategory => bookCategory.BookId == _bookId);
}