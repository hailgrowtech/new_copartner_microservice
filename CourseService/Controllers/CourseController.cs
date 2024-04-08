using CourseService.Dtos;
using CourseService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseBusinessProcessor _logic;
        private readonly ILogger<CourseController> _logger;
        //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

        public CourseController(ICourseBusinessProcessor logic, ILogger<CourseController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
        {
            this._logic = logic;
            this._logger = logger;
            // this._topicProducer = topicProducer;
        }
        /// <summary>
        /// Gets the list of all Experts.
        /// </summary>
        /// <returns>The list of Experts.</returns>
        // GET: api/Experts
        [HttpGet]
        public async Task<object> Get()
        {
            _logger.LogInformation("Fetching Experts Data..");
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
        [HttpGet("{Id}", Name = "Get")]
        public ActionResult<CourseReadDto> Get(Guid Id)
        {
            _logger.LogInformation("Fetching courses details for Id : " + Id.ToString());
            var courses = _logic.Get(Id);
            return courses != null ? (ActionResult<CourseReadDto>)Ok(courses) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(CourseCreateDto coursesDto)
        {
            var response = await _logic.Post(coursesDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<CourseCreateDto> courseDtoPatch)
        {
            var response = await _logic.Patch(Id, courseDtoPatch);
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
