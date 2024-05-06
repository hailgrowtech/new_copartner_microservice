using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AdminDashboardService.Dtos;
using AdminDashboardService.Logic;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExpertsAdvertisingAgencyController : ControllerBase
{
    private readonly IExpertsAdAgencyBusinessProcessor _logic;
    private readonly ILogger<ExpertsAdvertisingAgencyController> _logger;

    public ExpertsAdvertisingAgencyController(IExpertsAdAgencyBusinessProcessor logic, ILogger<ExpertsAdvertisingAgencyController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;


   
    [HttpGet(Name = "GetExpertsAdAgency")]
    public async Task<object> Get()
    {
            _logger.LogInformation("Fetching Ad Agencies Details Data..");
            var experts = await _logic.Get();
            return Ok(experts);        
    }

    /// <summary>
    /// Get an Advertising Agency RA From Advertising Agency ID.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/GetExpertsByAdAgencyId/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "GetExpertsByAdAgencyId")]
    public async Task<ActionResult<ExpertsAdAgencyDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Ad Agencies details for Id : " + Id.ToString());
        var expertsAdagency = await _logic.Get(Id);
        return expertsAdagency != null ? (ActionResult<ExpertsAdAgencyDto>)Ok(expertsAdagency) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(ExpertsAdAgencyCreateDto expertsAdAgencyCreateDto)
    {
        var response = await _logic.Post(expertsAdAgencyCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, ExpertsAdAgencyCreateDto expertsAdAgencyCreateDto)
    {
        var response = await _logic.Put(Id, expertsAdAgencyCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<ExpertsAdAgencyCreateDto> expertsAdAgencyPatch)
    {
        var response = await _logic.Patch(Id, expertsAdAgencyPatch);
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
        var adAgency = await _logic.Delete(Id);
        return adAgency != null ? Ok(adAgency) : NotFound();
    }
}
 