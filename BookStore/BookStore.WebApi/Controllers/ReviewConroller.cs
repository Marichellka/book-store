using BookStore.BLL.DTOs.Review;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController: ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ReviewDto>>> Get(
        [FromQuery] int? bookId, [FromQuery] int? userId)
    {
        ISpecification<Review> specification = new TrueSpecification<Review>();
        
        if (bookId != null)
        {
            specification = new AndSpecification<Review>(specification, new BookReviewSpecification(bookId.Value));
        }
        if (userId != null)
        {
            specification = new AndSpecification<Review>(specification, new UserReviewSpecification(userId.Value));
        }

        return Ok(await _reviewService.GetAll(specification));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        return Ok(await _reviewService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] NewReviewDto review)
    {
        return Ok(await _reviewService.Create(review));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ReviewDto review)
    {
        return Ok(await _reviewService.Update(review));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.Delete(id);
        return NoContent();
    }
}