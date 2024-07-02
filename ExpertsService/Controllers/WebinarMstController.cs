using ExpertService.Controllers;
using ExpertService.Dtos;
using ExpertService.Logic;
using ExpertsService.Dtos;
using ExpertsService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpertsService.Controllers
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
            _logger.LogInformation("Fetching Experts Data..");
            var webinars = await _logic.Get(page, pageSize);
            return Ok(webinars);
        }

        [HttpGet("{Id}", Name = "Get")]
        public async Task<ActionResult<WebinarMstReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching experts details for Id : " + Id.ToString());
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

    }
}
