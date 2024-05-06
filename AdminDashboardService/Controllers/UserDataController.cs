using AdminDashboardService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataListingBusinessProcessor _logic;
        private readonly ILogger<UserDataController> _logger;

        public UserDataController(IUserDataListingBusinessProcessor logic, ILogger<UserDataController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet("UserDataListing", Name = "GetUserDataListing")]
        public async Task<object> GetUserDataListing()
        {
            _logger.LogInformation("Fetching Dashboard GetUserDataListing Details Data..");
            var userListingData = await _logic.GetUserListing();
            return Ok(userListingData);
        }

        [HttpGet("UserFirstTimePaymentListing", Name = "GetUserFirstTimePaymentListing")]
        public async Task<object> GetUserFirstTimePaymentListing()
        {
            _logger.LogInformation("Fetching Dashboard GetUserFirstTimePaymentListing Details Data..");
            var userListingData = await _logic.GetFirstTimePaymentListing();
            return Ok(userListingData);
        }

        [HttpGet("UserSecondTimePaymentListing", Name = "GetUserSecondTimePaymentListing")]
        public async Task<object> GetUserSecondTimePaymentListing()
        {
            _logger.LogInformation("Fetching Dashboard GetUserSecondTimePaymentListing Details Data..");
            var userListingData = await _logic.GetSecondTimePaymentListing();
            return Ok(userListingData);
        }


    }
}
