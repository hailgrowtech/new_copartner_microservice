using CommonLibrary.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Dtos;
using SubscriptionService.Logic;

namespace SubscriptionService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly ISubscriberBusinessProcessor _logic;
    private readonly ILogger<SubscriberController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public SubscriberController(ISubscriberBusinessProcessor logic, ILogger<SubscriberController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
    {
        this._logic = logic;
        this._logger = logger;
        // this._topicProducer = topicProducer;
    }
    /// <summary>
    /// Gets the list of all Experts.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet]
    public async Task<object> Get()
    {
        _logger.LogInformation("Fetching Subscribers Data..");
        var subscribers = await _logic.Get();
        return Ok(subscribers);
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
    [HttpGet("{Id}")]
    public async Task<ActionResult<SubscriberReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching subscribers details for Id : " + Id.ToString());
        var subscribers = await _logic.Get(Id);
        return subscribers != null ? (ActionResult<SubscriberReadDto>)Ok(subscribers) : NotFound();
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
    [HttpGet("GetByUserId/{Id}", Name = "GetSubscriberByUserId")]
    public async Task<ActionResult<SubscriberReadDto>> GetByUserId(Guid Id)
    {
        _logger.LogInformation("Fetching subscribers details for Id : " + Id.ToString());
        var subscribers = await _logic.Get(Id);
        return subscribers != null ? (ActionResult<SubscriberReadDto>)Ok(subscribers) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(SubscriberCreateDto subscriberCreateDto)
    {
        var response = await _logic.Post(subscriberCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, SubscriberCreateDto subscriberCreateDto)
    {
        var response = await _logic.Put(Id, subscriberCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<SubscriberCreateDto> subscribersDtoPatch)
    {
        var response = await _logic.Patch(Id, subscribersDtoPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
}
