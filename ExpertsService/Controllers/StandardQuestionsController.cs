using ExpertService.Controllers;
using ExpertService.Dtos;
using ExpertService.Logic;
using ExpertsService.Dtos;
using ExpertsService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ExpertsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardQuestionsController : ControllerBase
    {
        private readonly IStandardQuestionsBusinessProcessor _logic;
        private readonly ILogger<StandardQuestionsController> _logger;

        public StandardQuestionsController(IStandardQuestionsBusinessProcessor logic, ILogger<StandardQuestionsController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        /// <summary>
        /// Gets the list of all Experts.
        /// </summary>
        /// <returns>The list of Experts.</returns>
        // GET: api/StandardQuestions
        [HttpGet]
        public async Task<object> Get(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Experts Data..");
            var standardQuestions = await _logic.Get(page, pageSize);
            return Ok(standardQuestions);
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
        [HttpGet("by-id/{Id}")]
        public async Task<ActionResult<StandardQuestionsReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching Standard Questions details for Id : " + Id.ToString());
            var experts = await _logic.Get(Id);
            return experts != null ? (ActionResult<StandardQuestionsReadDto>)Ok(experts) : NotFound();
        }

        [HttpGet("by-expert/{ExpertId}", Name = "GetQuestionByExpertId")]
        public async Task<object> GetByExpertId(Guid ExpertId, int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching Experts Data..");
            var standardQuestions = await _logic.Get(ExpertId, page, pageSize);
            return Ok(standardQuestions);
        }

        [HttpPost]
        public async Task<object> Post(StandardQuestionsCreateDto standardQuestionsCreateDto)
        {
            var response = await _logic.Post(standardQuestionsCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }


        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<StandardQuestionsCreateDto> standardQuestionsDtoPatch)
        {
            var response = await _logic.Patch(Id, standardQuestionsDtoPatch);
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
            var standardQuestion = await _logic.Delete(Id);
            return standardQuestion != null ? Ok(standardQuestion) : NotFound();
        }


    }
}
