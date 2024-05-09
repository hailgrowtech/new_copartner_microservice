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
        private readonly IAPListingDetailsBusinessProcessor _logicAPDetails;
        private readonly ILogger<AffiliatePartnerController> _logger;

        public APDashboardController(IAPListingBusinessProcessor logicAP, IAPListingDetailsBusinessProcessor logicAPDetails, ILogger<AffiliatePartnerController> logger)
        {
            _logicAP = logicAP;
            _logicAPDetails = logicAPDetails;
            _logger = logger;
        }

        [HttpGet("APListing", Name = "GetAPListing")]
        public async Task<object> GetAPListing()
        {
            _logger.LogInformation("Fetching APListing Data..");
            var response = await _logicAP.Get();
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet("APDetails", Name = "GetAPDetails")]
        public async Task<object> GetAPDetails()
        {
            _logger.LogInformation("Fetching APListing Data..");
            var response = await _logicAPDetails.Get();
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response);
        }

    }
}
