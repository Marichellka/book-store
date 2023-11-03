using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Category;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController: ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<BookDto>>> Get(
        [FromQuery] int? authorId, [FromQuery] int? publisherId,
        [FromQuery] IEnumerable<int>? categories)
    {
        ISpecification<Book> specification = new TrueSpecification<Book>();
        
        if (authorId != null)
        {
            specification = new AndSpecification<Book>(specification, new AuthorBooksSpecification(authorId.Value));
        }
        if (publisherId != null)
        {
            specification = new AndSpecification<Book>(specification, new PublisherBooksSpecification(publisherId.Value));
        }
        if (categories != null)
        {
            specification = new AndSpecification<Book>(specification, new BooksOfCategoriesSpecification(categories));
        }

        return Ok(await _bookService.GetAll(specification));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        return Ok(await _bookService.GetById(id));
    }
    
    [HttpGet("{id}/categories")]
    public async Task<ActionResult<BookDto>> GetBookCategories(int id)
    {
        return Ok(await _bookService.GetCategories(id));
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] NewBookDto book)
    {
        return Ok(await _bookService.Create(book));
    }
    
    [HttpPost("{id}/categories")]
    public async Task<IActionResult> AddCategory(int bookId, [FromBody] CategoryDto category)
    {
        return Ok(await _bookService.AddCategory(bookId, category));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BookDto book)
    {
        return Ok(await _bookService.Update(book));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.Delete(id);
        return NoContent();
    }
    
    [HttpDelete("{bookId}/categories/{categoryId}")]
    public async Task<IActionResult> DeleteCategory(int bookId, int categoryId)
    {
        await _bookService.DeleteCategory(bookId, categoryId);
        return NoContent();
    }
}