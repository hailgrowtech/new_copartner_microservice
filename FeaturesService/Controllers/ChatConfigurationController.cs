using FeaturesService.Dtos;
using FeaturesService.Logic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FeaturesService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatConfigurationController : ControllerBase
{
    private readonly IChatBusinessProcessor _logic;
    private readonly ILogger<ChatConfigurationController> _logger;

    public ChatConfigurationController(IChatBusinessProcessor logic, ILogger<ChatConfigurationController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpGet]
    public async Task<object> Get(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Chat Experts Data..");
        var chatPlan = await _logic.Get(page, pageSize);
        return Ok(chatPlan);

    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<WebinarBookingReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Chat Plan Details for Id : " + Id.ToString());
        var webinars = await _logic.Get(Id);
        return webinars != null ? (ActionResult<WebinarBookingReadDto>)Ok(webinars) : NotFound();
    }


    /// <summary>
    /// Creates a new Chat Plan based on the provided data.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ChatConfig
    ///     {
    ///         "PlanType": "F" // F For Free, P For Payment 
    ///         "Duration:"10" // in minutes
    ///         Other Post Parameter as per required
    ///     }
    /// </remarks>
    /// <param name="chatPlanCreateDto">The data required to create a new chat plan.</param>
    /// 
    [HttpPost]
    public async Task<object> Post(ChatPlanCreateDto chatPlanCreateDto)
    {
        var response = await _logic.Post(chatPlanCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<ChatPlanCreateDto> chatPlanPatch)
    {
        var response = await _logic.Patch(Id, chatPlanPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    /// <summary>
    /// Creates a new Chat User based on the provided data.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ChatConfig
    ///     {
    ///         "Id": "Guid" // User and Experts Id i.e Guid
    ///         "UserType": "RA / UR" // For User ="UR" and Experts ="RA"
    ///         "Username": "MobileNo and  Name " // Mobileno for user Name for Experts
    ///     }
    /// </remarks>

    /// 
    [HttpPost("PostChatUser", Name = "PostChatUser")]
    public async Task<object> PostChatUser(ChatUserCreateDto chatUserCreateDto)
    {
        var response = await _logic.PostChatUser(chatUserCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    /// <summary>
    /// Get the list of chat users/experts with whom the user/expert has chatted by user/expert ID.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ChatConfig
    ///     {
    ///         "Id": "Guid" // User or Experts Id i.e Guid
    ///     }
    /// </remarks>

    /// 
    [HttpGet("GetChatUsersById/{Id}", Name = "GetChatUsersById")]
    public async Task<object> GetChatUserById(Guid Id)
    {
        var response = await _logic.GetChatUserById(Id);

        if (response.IsSuccess)
        {
           //z Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

}
