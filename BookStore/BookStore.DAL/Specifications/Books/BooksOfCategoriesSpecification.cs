using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Books;

public class BooksOfCategoriesSpecification: ISpecification<Book>
{
    private readonly IEnumerable<int> _categoriesIds;

    public BooksOfCategoriesSpecification(IEnumerable<int> categoryIds)
    {
        _categoriesIds = categoryIds;
    }
    
    public Expression<Func<Book, bool>> SpecificationExpression => book => 
        book.Categories != null && book.Categories.Any(category => _categoriesIds.Contains(category.CategoryId));
}