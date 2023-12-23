using BookStore.BLL.DTOs.Category;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController: ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<CategoryDto>>> Get()
    {
        return Ok(await _categoryService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        return Ok(await _categoryService.GetById(id));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] NewCategoryDto category)
    {
        return Ok(await _categoryService.Create(category));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CategoryDto category)
    {
        return Ok(await _categoryService.Update(category));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);
        return NoContent();
    }
}