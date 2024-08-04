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
    public async Task<ActionResult<ChatPlanReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching Chat Plan Details for Id : " + Id.ToString());
        var webinars = await _logic.Get(Id);
        return webinars != null ? (ActionResult<ChatPlanReadDto>)Ok(webinars) : NotFound();
    }

    /// <summary>
    /// Get ChatPlanByExpertsID
    /// </summary>
    /// <param name="Id">ExpertsId</param>
    /// <returns></returns>

    [HttpGet("GetChatUsersByExpertsId/{Id}", Name = "GetChatUsersByExpertsId")]
    public async Task<ActionResult<ChatPlanReadDto>> GetChatPlanByExpertsId(Guid Id, int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Chat Plan By ExpertsId : " + Id.ToString());
        var webinars = await _logic.GetChatPlanByExpertsId(Id, page, pageSize);
        return webinars != null ? (ActionResult<ChatPlanReadDto>)Ok(webinars) : NotFound();
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
    ///         "discountValidFrom: "2024-07-30T09:56:42.580Z" DateTime Must be in UTC
    ///         "discountValidTo: "2024-07-30T10:56:42.580Z" DateTime Must be in UTC
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
    /// Delete Chat Plan on the basis of Id 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>

    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var expert = await _logic.Delete(Id);
        return expert != null ? Ok(expert) : NotFound();
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

    /// <summary>
    /// Post Free Chat Plan When User join Free Chat Plan.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ChatConfig
    ///     {
    ///         "UserId": "Guid" // User Id i.e Guid
    ///         "ExpertsId": "Guid" //Experts Id i.e Guid
    ///         "Availed": "bollean" // send true when called
    ///     }
    /// </remarks>

    /// 
    [HttpPost("PostFreeChat", Name = "PostFreeChat")]
    public async Task<object> PostFreeChat(FreeChatCreateDto freeChatCreateDto)
    {
        var response = await _logic.PostFreeChat(freeChatCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    /// <summary>
    /// Get the list of freechat done by User.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ChatConfig
    ///     {
    ///         "Id": "Guid" // User Id i.e Guid
    ///     }
    /// </remarks>

    /// 
    [HttpGet("GetFreeChatUser/{Id}", Name = "GetFreeChatUserById")]
    public async Task<object> GetFreeChatUser(Guid Id)
    {
        var response = await _logic.GetFreeChatUser(Id);

        if (response.IsSuccess)
        {
            //z Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

}
