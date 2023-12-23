using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Books;

namespace BookStore.Tests.Specification;

public class BookSpecificationTests: SpecificationTestBase
{
    [Test]
    public void AuthorBooksSpecification_Satisfies()
    {
        AuthorBooksSpecification specification = new(authorId:1);

        bool result = IsSatisfiedBy(specification, new Book(){AuthorId = 1});
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void AuthorBooksSpecification_DoesNotSatisfy()
    {
        AuthorBooksSpecification specification = new(authorId:1);

        bool result = IsSatisfiedBy(specification, new Book(){AuthorId = 2});
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void BookCategoriesSpecification_Satisfies()
    {
        BookCategoriesSpecification specification = new(1);

        bool result = IsSatisfiedBy(specification, new Category(){BookCategories = new []{new BookCategory(){BookId = 1}}});
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void BookCategoriesSpecification_DoesNotSatisfy()
    {
        BookCategoriesSpecification specification = new(1);

        bool result = IsSatisfiedBy(specification, new Category(){});

        Assert.That(result, Is.False);
    }
    
    [Test]
    public void PublisherBooksSpecification_Satisfies()
    {
        PublisherBooksSpecification specification = new(publisherId:1);

        bool result = IsSatisfiedBy(specification, new Book(){PublisherId = 1});
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void PublisherBooksSpecification_DoesNotSatisfy()
    {
        PublisherBooksSpecification specification = new(publisherId:1);

        bool result = IsSatisfiedBy(specification, new Book(){PublisherId = 2});
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void CategoryBooksSpecification_Satisfies()
    {
        CategoryBooksSpecification specification = new(1);

        bool result = IsSatisfiedBy(specification, new Book(){Categories = new []{new BookCategory(){CategoryId = 1}}});

        Assert.That(result, Is.True);
    }
    
    [Test]
    public void CategoryBooksSpecification_DoesNotSatisfy()
    {
        CategoryBooksSpecification specification = new(1);

        bool result = IsSatisfiedBy(specification, new Book(){});

        Assert.That(result, Is.False);
    }
    
    [Test]
    public void BooksOfCategoriesSpecification_Satisfies()
    {
        BooksOfCategoriesSpecification specification = new(new []{1});

        bool result = IsSatisfiedBy(specification, new Book(){Categories = new []{new BookCategory(){CategoryId = 1}}});

        Assert.That(result, Is.True);
    }
    
    [Test]
    public void BooksOfCategoriesSpecification_DoesNotSatisfy()
    {        
        BooksOfCategoriesSpecification specification = new(new []{1});

        bool result = IsSatisfiedBy(specification, new Book(){});

        Assert.That(result, Is.False);
    }
}