using AdminDashboardService.Dtos;
using AdminDashboardService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramMessageController : ControllerBase
    {
        private readonly ITelegramMessageBusinessProcessor _logic;
        private readonly ILogger<TelegramMessageController> _logger;

        public TelegramMessageController(ITelegramMessageBusinessProcessor logic, ILogger<TelegramMessageController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Telegram Messages Data..");
            var experts = await _logic.Get(page, pageSize);
            return Ok(experts);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TelegramMessageReadDto>> Get(Guid Id, string userType, int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Telegram Messages for Id : " + Id.ToString());
            var experts = await _logic.Get(Id, userType, page, pageSize);
            return experts != null ? (ActionResult<TelegramMessageReadDto>)Ok(experts) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(TelegramMessageCreateDto telegramMessageCreateDto)
        {
            var response = await _logic.Post(telegramMessageCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<TelegramMessageCreateDto> telegramChannelPatch)
        {
            var response = await _logic.Patch(Id, telegramChannelPatch);
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
