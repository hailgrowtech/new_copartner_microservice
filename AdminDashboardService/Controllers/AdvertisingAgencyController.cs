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
public class AdvertisingAgencyController : ControllerBase
{
    private readonly IAdAgencyDetailsBusinessProcessor _logic;
    private readonly ILogger<AdvertisingAgencyController> _logger;

    public AdvertisingAgencyController(IAdAgencyDetailsBusinessProcessor logic, ILogger<AdvertisingAgencyController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;


    /// <summary>
    /// Gets the list of all AdvertisingAgency.
    /// </summary>
    /// <returns>The list of AdvertisingAgency.</returns>
    // GET: api/AdvertisingAgency
    [HttpGet]
    public async Task<object> Get()
    {
            _logger.LogInformation("Fetching Ad Agencies Details Data..");
            var experts = await _logic.Get();
            return Ok(experts);        
    }

    /// <summary>
    /// Get an AdvertisingAgency.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/AdvertisingAgency/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "Get")]
    public async Task<ActionResult<AdAgencyDetailsDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Ad Agencies details for Id : " + Id.ToString());
        var adagency = await _logic.Get(Id);
        return adagency != null ? (ActionResult<AdAgencyDetailsDto>)Ok(adagency) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(AdAgencyDetailsCreateDto adAgencyDetailsCreateDto)
    {
        var response = await _logic.Post(adAgencyDetailsCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, AdAgencyDetailsCreateDto adAgencyDetailsCreateDto)
    {
        var response = await _logic.Put(Id, adAgencyDetailsCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<AdAgencyDetailsCreateDto> adAgencyPatch)
    {
        var response = await _logic.Patch(Id, adAgencyPatch);
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
 