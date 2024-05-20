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
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionBusinessProcessor _logic;
    private readonly ILogger<SubscriptionController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public SubscriptionController(ISubscriptionBusinessProcessor logic, ILogger<SubscriptionController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
    {
        this._logic = logic;
        this._logger = logger;
        // this._topicProducer = topicProducer;
    }
    /// <summary>
    /// Gets the list of all Subscription.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet]
    public async Task<object> Get()
    {
        _logger.LogInformation("Fetching Subscription Data..");
        var experts = await _logic.Get();
        return Ok(experts);
    }

    /// <summary>
    /// Get Subscription.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Experts/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "Get")]
    public async Task<ActionResult<SubscriptionReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Subscription details for Id : " + Id.ToString());
        var subscriptionMsts = await _logic.Get(Id);
        return subscriptionMsts != null ? (ActionResult<SubscriptionReadDto>)Ok(subscriptionMsts) : NotFound();
    }

    /// <summary>
    /// Get Subscription By Experts Id Experts.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Experts/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "GetByExpertsId")]
    public async Task<ActionResult<SubscriptionReadDto>> GetByExpertsId(Guid Id)
    {
        _logger.LogInformation("Fetching Subscription details for Id : " + Id.ToString());
        var subscriptionMsts = await _logic.Get(Id);
        return subscriptionMsts != null ? (ActionResult<SubscriptionReadDto>)Ok(subscriptionMsts) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(SubscriptionCreateDto subscriptionCreateDto)
    {
        var response = await _logic.Post(subscriptionCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id,SubscriptionCreateDto subscriptionCreateDto)
    {
        var response = await _logic.Put(Id,subscriptionCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<SubscriptionCreateDto> subscriptionDtoPatch)
    {
        var response = await _logic.Patch(Id, subscriptionDtoPatch);
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
        var user = await _logic.Delete(Id);
        return user != null ? Ok(user) : NotFound();
    }
}
