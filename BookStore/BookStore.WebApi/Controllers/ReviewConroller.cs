using BookStore.BLL.DTOs.Review;
using BookStore.BLL.Services;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Reviews;
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] NewReviewDto review)
    {
        if (User.GetUserId() != review.UserId)
            return Forbid();
        
        return Ok(await _reviewService.Create(review));
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ReviewDto review)
    {
        if (User.GetUserId() != review.UserId)
            return Forbid();
        
        return Ok(await _reviewService.Update(review));
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.Delete(id);
        return NoContent();
    }
}