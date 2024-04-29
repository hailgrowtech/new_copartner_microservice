using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using BlogService.Logic;
using BlogService.Dtos;

namespace BlogService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IBlogBusinessProcessor _logic;
    private readonly ILogger<BlogsController> _logger;

    public BlogsController(IBlogBusinessProcessor logic, ILogger<BlogsController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;


    /// <summary>
    /// Gets the list of all Experts.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet]
    public async Task<object> Get()
    {
            _logger.LogInformation("Fetching Experts Data..");
            var experts = await _logic.Get();
            return Ok(experts);        
    }

    /// <summary>
    /// Get an Experts.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Experts/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "Get")]
    public async Task<ActionResult<BlogReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching blogs details for Id : " + Id.ToString());
        var blogs = await _logic.Get(Id);
        return blogs != null ? (ActionResult<BlogReadDto>)Ok(blogs) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(BLogCreateDto blogsDto)
    {
        var response = await _logic.Post(blogsDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, BLogCreateDto bLogCreateDto)
    {
        var response = await _logic.Put(Id, bLogCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<BLogCreateDto> blogsDtoPatch)
    {
        var response = await _logic.Patch(Id, blogsDtoPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var blog = await _logic.Delete(Id);
        return blog != null ? Ok(blog) : NotFound();
    }
}
 