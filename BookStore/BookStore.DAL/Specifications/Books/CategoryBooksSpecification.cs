using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Books;

public class CategoryBooksSpecification: ISpecification<Book>
{
    private readonly int _categoryId;

    public CategoryBooksSpecification(int categoryId)
    {
        _categoryId = categoryId;
    }
    
    public Expression<Func<Book, bool>> SpecificationExpression => book => 
        book.Categories != null && book.Categories.Any(category => category.CategoryId == _categoryId);
}