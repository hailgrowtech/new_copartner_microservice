using FeaturesService.Dtos;
using FeaturesService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FeaturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebinarMstController : ControllerBase
    {
        private readonly IWebinarMstBusinessProcessor _logic;
        private readonly ILogger<WebinarMstController> _logger;
        //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

        public WebinarMstController(IWebinarMstBusinessProcessor logic, ILogger<WebinarMstController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching WebinarMst Data Data..");
            var webinars = await _logic.Get(page, pageSize);
            return Ok(webinars);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<WebinarMstReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching webinar mst details for Id : " + Id.ToString());
            var webinars = await _logic.Get(Id);
            return webinars != null ? (ActionResult<WebinarMstReadDto>)Ok(webinars) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(WebinarMstCreateDto webinarMstCreateDto)
        {
            var response = await _logic.Post(webinarMstCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<WebinarMstCreateDto> webinarmstpatch)
        {
            var response = await _logic.Patch(Id, webinarmstpatch);
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
}
