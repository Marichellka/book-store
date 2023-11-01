using BookStore.BLL.DTOs.Publisher;
using BookStore.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublisherController: ControllerBase
{
    private readonly PublisherService _publisherService;

    public PublisherController(PublisherService publisherService)
    {
        _publisherService = publisherService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<PublisherDto>>> Get()
    {
        return Ok(await _publisherService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> GetById(int id)
    {
        return Ok(await _publisherService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> Create([FromBody] NewPublisherDto publisher)
    {
        return Ok(await _publisherService.Create(publisher));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PublisherDto publisher)
    {
        return Ok(await _publisherService.Update(publisher));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _publisherService.Delete(id);
        return NoContent();
    }
}