using AdminDashboardService.Dtos;
using AdminDashboardService.Logic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardService.Controllers;


//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RelationshipManagerController : ControllerBase
{
    private readonly IRelationshipManagerBusinessProcessor _logic;
    private readonly ILogger<RelationshipManagerController> _logger;

    public RelationshipManagerController(IRelationshipManagerBusinessProcessor logic, ILogger<RelationshipManagerController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;


    /// <summary>
    /// Gets the list of all Relationship Manager.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/Experts
    [HttpGet(Name = "GetRelationshipManager")]
    public async Task<object> Get(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Relationship Manager Data..");
        var marketingContent = await _logic.Get(page, pageSize);
        return Ok(marketingContent);
    }

    /// <summary>
    /// Get Relationship Manager.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Experts/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}", Name = "GetRelationshipManagerById")]
    public async Task<ActionResult<RelationshipManagerReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Relationship Manager for Id : " + Id.ToString());
        var relationshipManager = await _logic.Get(Id);
        return relationshipManager != null ? (ActionResult<RelationshipManagerReadDto>)Ok(relationshipManager) : NotFound();
    }

    /// <summary>
    /// Get Relationship Manager By RA AP ID
    /// </summary>
    /// <param name="Id">RA AP ID </param>
    /// <param name="UserType">RA or AP</param>
    /// <returns></returns>
    [HttpGet("GetByUserId", Name = "GetByUserId")]
    public async Task<ActionResult<RelationshipManagerDto>> GetByUserId(Guid Id, string UserType)
    {
        _logger.LogInformation("Fetching Relationship Manager for Id : " + Id.ToString());
        var relationshipManager = await _logic.GetByUserId(Id, UserType);
        return relationshipManager != null ? (ActionResult<RelationshipManagerDto>)Ok(relationshipManager) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(RelationshipManagerCreateDto relationshipManagerDto)
    {
        var response = await _logic.Post(relationshipManagerDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, RelationshipManagerCreateDto relationshipManagerCreateDto)
    {
        var response = await _logic.Put(Id, relationshipManagerCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<RelationshipManagerCreateDto> relationshipManagerdto)
    {
        var response = await _logic.Patch(Id, relationshipManagerdto);
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
        var relationshipManager = await _logic.Delete(Id);
        return relationshipManager != null ? Ok(relationshipManager) : NotFound();
    }
}
