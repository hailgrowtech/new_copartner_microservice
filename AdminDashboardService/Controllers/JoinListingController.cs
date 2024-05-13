using AdminDashboardService.Dtos;
using AdminDashboardService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardService.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class JoinListingController : ControllerBase
    {
        private readonly IJoinBusinessProcessor _logic;
        private readonly ILogger<AdvertisingAgencyController> _logger;

        public JoinListingController(IJoinBusinessProcessor logic, ILogger<AdvertisingAgencyController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        // GET: api/Experts
        [HttpGet(Name = "GetJoins")]
        public async Task<object> Get()
        {
            _logger.LogInformation("Fetching Joins Data..");
            var experts = await _logic.Get();
            return Ok(experts);
        }

        /// <summary>
        /// Get an Experts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET : api/Experts/1
        /// </remarks>
        /// <param name="Id"></param>
        [HttpGet("{Id}", Name = "GetJoinsById")]
        public async Task<ActionResult<JoinReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching joins details for Id : " + Id.ToString());
            var joins = await _logic.Get(Id);
            return joins != null ? (ActionResult<JoinReadDto>)Ok(joins) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(JoinCreateDto joinsDto)
        {
            var response = await _logic.Post(joinsDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("{Id:guid}")]
        public async Task<object> Put(Guid Id, JoinCreateDto joinCreateDto)
        {
            var response = await _logic.Put(Id, joinCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<JoinCreateDto> joinsDtoPatch)
        {
            var response = await _logic.Patch(Id, joinsDtoPatch);
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
            var blog = await _logic.Delete(Id);
            return blog != null ? Ok(blog) : NotFound();
        }
    }
}
