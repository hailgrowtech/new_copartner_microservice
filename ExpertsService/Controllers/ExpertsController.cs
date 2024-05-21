using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ExpertService.Dtos;
using ExpertService.Logic;
using CommonLibrary.Authorization;
using ExpertsService.Logic;

namespace ExpertService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExpertsController : ControllerBase
{
    private readonly IExpertsBusinessProcessor _logic;
    private readonly ILogger<ExpertsController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public ExpertsController(IExpertsBusinessProcessor logic, ILogger<ExpertsController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

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
    public async Task<ActionResult<ExpertReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching experts details for Id : " + Id.ToString());
        var experts = await _logic.Get(Id);
        return experts != null ? (ActionResult<ExpertReadDto>)Ok(experts) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(ExpertsCreateDto expertsDto)
    {
        var response = await _logic.Post(expertsDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, ExpertsCreateDto expertsCreateDto)
    {
        var response = await _logic.Put(Id, expertsCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<ExpertsCreateDto> expertsDtoPatch)
    {
        var response = await _logic.Patch(Id, expertsDtoPatch);
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
        var expert = await _logic.Delete(Id);
        return expert != null ? Ok(expert) : NotFound();
    }


    [HttpGet("GenerateReferralLink/{Id}", Name = "GenerateReferralLink")]
    public async Task<ActionResult<ExpertReadDto>> GenerateReferralLink(Guid Id)
    {
        _logger.LogInformation("Fetching expert Referral Link details for Id : " + Id.ToString());
        var expert = await _logic.GenerateReferralLink(Id);
        return expert != null ? (ActionResult<ExpertReadDto>)Ok(expert) : NotFound();
    }

}
