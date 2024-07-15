using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Dtos;
using SubscriptionService.Logic;

namespace SubscriptionService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MiniSubscriptionController : ControllerBase
    {
        private readonly IMiniSubscriptionByLinkBusinessProcessor _logic;
        private readonly ILogger<MiniSubscriptionController> _logger;
        //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

        public MiniSubscriptionController(IMiniSubscriptionByLinkBusinessProcessor logic, ILogger<MiniSubscriptionController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
        {
            this._logic = logic;
            this._logger = logger;
            // this._topicProducer = topicProducer;
        }
        /// <summary>
        /// Gets the list of all Subscription.
        /// </summary>
        /// <returns>The list of Experts.</returns>
        // GET: api/Experts
        [HttpGet]
        public async Task<object> Get()
        {
            _logger.LogInformation("Fetching MiniSubscription Data..");
            var miniSubscriptionLink = await _logic.Get();
            return Ok(miniSubscriptionLink);
        }

        /// <summary>
        /// Get Subscription.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET : api/Experts/1
        /// </remarks>
        /// <param name="Id"></param>
        [HttpGet("{Id}")]
        public async Task<ActionResult<MiniSubscriptionReadDto>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching Mini Subscription details for Id : " + Id.ToString());
            var miniSubscriptionLinkMsts = await _logic.Get(Id);
            return miniSubscriptionLinkMsts != null ? (ActionResult<MiniSubscriptionReadDto>)Ok(miniSubscriptionLinkMsts) : NotFound();
        }

        

        [HttpPost]
        public async Task<object> Post(MiniSubscriptionCreateDto miniSubscriptionCreateDto)
        {
            var response = await _logic.Post(miniSubscriptionCreateDto);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        


        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var user = await _logic.Delete(Id);
            return user != null ? Ok(user) : NotFound();
        }
    }
}
