using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Dtos;
using SubscriptionService.Logic;

namespace SubscriptionService.Controllers;

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
    /// Gets the list of all Experts.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet]
    public async Task<object> Get()
    {
        _logger.LogInformation("Fetching SubscriptionMst Data..");
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
    public async Task<ActionResult<SubscriptionReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching SubscriptionMst details for Id : " + Id.ToString());
        var subscriptionMsts = await _logic.Get(Id);
        return subscriptionMsts != null ? (ActionResult<SubscriptionReadDto>)Ok(subscriptionMsts) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(SubscriptionCreateDto subscriptionMstCreateDto)
    {
        var response = await _logic.Post(subscriptionMstCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<SubscriptionCreateDto> subscriptionMstDtoPatch)
    {
        var response = await _logic.Patch(Id, subscriptionMstDtoPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var user = await _logic.Delete(Id);
        return user != null ? Ok(user) : NotFound();
    }
}
