using ExpertsService.Dtos;
using ExpertsService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ExpertsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertAvailabilityController : ControllerBase
    {
        private readonly IExpertAvailabilityBusinessProcessor _logic;
        private readonly ILogger<ExpertAvailabilityController> _logger;

        public ExpertAvailabilityController(IExpertAvailabilityBusinessProcessor logic, ILogger<ExpertAvailabilityController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Expert Availability Data..");
            var webinars = await _logic.Get(page, pageSize);
            return Ok(webinars);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpertAvailabilityReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching Expert Availability details for Id : " + Id.ToString());
            var webinars = await _logic.Get(Id);
            return webinars != null ? (ActionResult<ExpertAvailabilityReadDto>)Ok(webinars) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(ExpertAvailabilityCreateDto expertAvailabilityCreateDto)
        {
            var response = await _logic.Post(expertAvailabilityCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<ExpertAvailabilityCreateDto> expertAvailabilityPatch)
        {
            var response = await _logic.Patch(Id, expertAvailabilityPatch);
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
