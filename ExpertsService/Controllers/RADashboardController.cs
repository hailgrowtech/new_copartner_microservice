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
public class RADashboardController : ControllerBase
{
    private readonly IRAListingBusinessProcessor _logicRA;
    private readonly IRADetailsBusinessProcessor _logicRADetails;
    private readonly ILogger<ExpertsController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public RADashboardController( IRAListingBusinessProcessor logicRA, IRADetailsBusinessProcessor logicRADetails, ILogger<ExpertsController> logger)
    {
        _logicRA = logicRA;
        _logicRADetails = logicRADetails;
        _logger = logger;
    }

    /// <summary>
    /// Gets RA Listing In Dasboard.
    /// </summary>
    /// <returns>The RA Listing in Dashboard</returns>
    // GET: api/Experts
    [HttpGet("DashboardRAListing", Name = "GetDashboardRAListing")]
    public async Task<object> GetRADashboardListing(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Dashboard RA Listing Data..");
        var raListing = await _logicRA.Get(page, pageSize);
        return Ok(raListing);
    }

    /// <summary>
    /// Gets RA Listing Data For perticular RA & RA Details Screen in Dashboard.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/GetDashboardRAListingData/1
    /// </remarks>
    /// <param name="Id"></param>
    /// /// <param name="Id">RA Guid</param>
    [HttpGet("GetDashboardRAListingData/{Id}", Name = "GetDashboardRAListingData")]
    public async Task<ActionResult<ExpertReadDto>> GetDashboardRAListingData(Guid Id, int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching experts details for Id : " + Id.ToString());
        var experts = await _logicRA.Get(Id);
        return experts != null ? (ActionResult<ExpertReadDto>)Ok(experts) : NotFound();
    }

    /// <summary>
    /// Gets income listing details of all Experts.
    /// </summary>
    /// <returns>The detail list of Experts.</returns>
    // GET: api/Experts
    [HttpGet("DashboardRADetails", Name = "GetRAListingDetails")]
    public async Task<object> GetRAListingDetails(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Dashboard RA Listing Details Data..");
        var raListingDetails = await _logicRADetails.Get(page, pageSize);
        return Ok(raListingDetails);
    }
}
