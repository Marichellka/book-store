using BookStore.BLL.DTOs.Author;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController: ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<AuthorDto>>> Get()
    {
        return Ok(await _authorService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        return Ok(await _authorService.GetById(id));
    }
    
    [HttpGet("{id}/books")]
    public async Task<ActionResult<ICollection<BookDto>>> GetBooks(int id)
    {
        return Ok(await _authorService.GetBooks(id));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create([FromBody] NewAuthorDto author)
    {
        return Ok(await _authorService.Create(author));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AuthorDto author)
    {
        return Ok(await _authorService.Update(author));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorService.Delete(id);
        return NoContent();
    }
}