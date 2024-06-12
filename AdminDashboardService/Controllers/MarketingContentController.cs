using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AdminDashboardService.Logic;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MarketingContentController : ControllerBase
{
    private readonly IMarketingContentBusinessProcessor _logic;
    private readonly ILogger<MarketingContentController> _logger;

    public MarketingContentController(IMarketingContentBusinessProcessor logic, ILogger<MarketingContentController> logger)
    {
        _logic = logic;
        _logger = logger;
    }



    /// <summary>
    /// Gets the list of all Experts.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet(Name = "GetMarketingContent")]
    public async Task<object> Get(int page = 1, int pageSize = 10)
    {
            _logger.LogInformation("Fetching Marketing Content Data..");
            var marketingContent = await _logic.Get(page, pageSize);
            return Ok(marketingContent);        
    }

    [HttpGet("GetMarketingContentByContentType", Name = "GetMarketingContentByContentType")]
    public async Task<object> Get(int page = 1, int pageSize = 10, string contentType = null)
    {
        _logger.LogInformation("Fetching Marketing Content Data..");
        var marketingContent = await _logic.Get(page, pageSize, contentType);
        return Ok(marketingContent);
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
    [HttpGet("{Id}", Name = "GetMarketingContentById")]
    public async Task<ActionResult<MarketingContentReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching marketing content details for Id : " + Id.ToString());
        var marketingContent = await _logic.Get(Id);
        return marketingContent != null ? (ActionResult<MarketingContentReadDto>)Ok(marketingContent) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(MarketingContentCreateDto marketingContentDto)
    {
        var response = await _logic.Post(marketingContentDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, MarketingContentCreateDto marketingContentCreateDto)
    {
        var response = await _logic.Put(Id, marketingContentCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<MarketingContentCreateDto> marketingContentDtoPatch)
    {
        var response = await _logic.Patch(Id, marketingContentDtoPatch);
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

    
}
