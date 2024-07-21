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
        private const string CopartnerAppId = "67d5feceed9d4a319444345b0c034182";
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

        [HttpPost("GenerateWebinarToken")]
        public IActionResult GenerateWebinarToken([FromBody] WebinarRequest request)
        {
            if (string.IsNullOrEmpty(request.ChannelName))
            {
                request.ChannelName = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrEmpty(request.UId))
            {
                request.UId = new Random().Next(1, int.MaxValue).ToString();
            }

            var token = _logic.GenerateToken(request.ChannelName, request.UId, request.IsHost);

            // Construct the Agora link
            // string agoraLink = $"https://console.agora.io/join?channel={request.ChannelName}&token={token}&uid={request.Uid}";

           // var agoraLink = $"{Request.Scheme}://{Request.Host}/api/Webinar/JoinWebinar?channelName={request.ChannelName}&uid={request.Uid}&token={token}";
            //var agoraLink = Url.Action("JoinWebinar", "Webinar", new { channelName = request.ChannelName, uid = request.Uid, token }, Request.Scheme);


            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Failed to generate webinar token.");
            }

           // return Ok(token);

            return Ok(new { agoraToken = token });
        }

        [HttpGet("JoinWebinar")]
        public IActionResult JoinWebinar([FromQuery] string channelName, [FromQuery] string uid, [FromQuery] string token)
        {
            var joinInfo = new { ChannelName = channelName, Uid = uid, Token = token, AppId = CopartnerAppId };
            return Ok(joinInfo);
        }
    }
    public class WebinarRequest
    {
        public string ChannelName { get; set; }
        public string UId { get; set; }
        public bool IsHost { get; set; } = true;
    }
}
