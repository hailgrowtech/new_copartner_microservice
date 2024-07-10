using FeaturesService.Dtos;
using FeaturesService.Dtos;
using FeaturesService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FeaturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebinarBookingController : ControllerBase
    {
        private readonly IWebinarBookingBusinessProcessor _logic;
        private readonly ILogger<WebinarBookingController> _logger;
        //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

        public WebinarBookingController(IWebinarBookingBusinessProcessor logic, ILogger<WebinarBookingController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Experts Data..");
            var webinars = await _logic.Get(page, pageSize);
            return Ok(webinars);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<WebinarBookingReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching webinar booking details for Id : " + Id.ToString());
            var webinars = await _logic.Get(Id);
            return webinars != null ? (ActionResult<WebinarBookingReadDto>)Ok(webinars) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(WebinarBookingCreateDto webinarBookingCreateDto)
        {
            var response = await _logic.Post(webinarBookingCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<WebinarBookingCreateDto> webinarbookingpatch)
        {
            var response = await _logic.Patch(Id, webinarbookingpatch);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
