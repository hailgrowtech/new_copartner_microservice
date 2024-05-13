using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AffiliatePartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APDashboardController : ControllerBase
    {
        private readonly IAPListingBusinessProcessor _logicAP;
        private readonly IAPDetailsBusinessProcessor _logicAPDetails;
        private readonly ILogger<AffiliatePartnerController> _logger;

        public APDashboardController(IAPListingBusinessProcessor logicAP, IAPDetailsBusinessProcessor logicAPDetails, ILogger<AffiliatePartnerController> logger)
        {
            _logicAP = logicAP;
            _logicAPDetails = logicAPDetails;
            _logger = logger;
        }

        /// <summary>
        /// Gets AP Listing Data For particular AP and AP Details Screen in Dashboard.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET : api/GetDashboardRAListingData/1
        /// </remarks>
        /// <param name="Id"></param>
        /// /// <param name="Id">RA Guid</param>
        [HttpGet("DashboardAPListing", Name = "GetAPListing")]
        public async Task<object> GetDashboardAPListing(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching APListing Data..");
            var response = await _logicAP.Get(page, pageSize);
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response);
        }

        /// <summary>
        /// Gets AP Listing Data For particular AP and AP Details Screen in Dashboard.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET : api/GetDashboardRAListingData/1
        /// </remarks>
        /// <param name="Id"></param>
        /// /// <param name="Id">RA Guid</param>
        [HttpGet("GetDashboardAPListingData/{Id}", Name = "GetDashboardAPListingData")]
        public async Task<ActionResult<APListingDataDto>> GetDashboardAPListingData(Guid Id, int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Dashboard AP details for Id : " + Id.ToString());
            var experts = await _logicAP.Get(Id,page,pageSize);
            return experts != null ? (ActionResult<APListingDataDto>)Ok(experts) : NotFound();
        }

        [HttpGet("DashobaordAPDetails", Name = "GetDashboardAPDetails")]
        public async Task<object> GetDashboardAPDetails(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Dashboard AP DetailsData..");
            var response = await _logicAPDetails.Get(page, pageSize);
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response);
        }

    }
}
