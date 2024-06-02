using CommonLibrary.Authorization;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Dtos;
using AuthenticationService.Logic;
using MassTransit.Mediator;

namespace AuthenticationService.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserBusinessProcessor _logic;
    private readonly ILogger<UsersController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public UsersController(IUserBusinessProcessor logic, ILogger<UsersController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
    {
        this._logic = logic;
        this._logger = logger;

       // this._topicProducer = topicProducer;
    }
    /// <summary>
    /// Gets the list of all Users - RA/AP/SubAdmin. 
    /// </summary>
    /// <returns>The list of Users.</returns>
    // GET: api/User
    [HttpGet]
    public async Task<object> Get(string userType, int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching User Data..");
        var users = await _logic.Get(userType, page, pageSize);
        return Ok(users);
    }

    /// <summary>
    /// Get an User.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/User/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching user details for Id : " + Id.ToString());
        var user = await _logic.Get(Id);
        return  user != null ? (ActionResult<UserReadDto>)Ok(user) : NotFound();
    }
    /// <summary>
    /// Create all Users - RA,AP,SubAdmin. In StackholderId put expertsId, APId, SAId 
    /// </summary>
    /// <returns>The list of Users.</returns>
    // POST: api/User
    [HttpPost]
    public async Task<object> Post(UserCreateDto stackholderDto)
    {
        var response = await _logic.Post(stackholderDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);
            //await _topicProducer.Produce(new UserCreatedEventDTO
            //{
            //    Email = userDto.Email,
            //    Mobile = userDto.MobileNumber,
            //    Password = userDto.Password,
            //    UserId = guid
            //});
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, UserCreateDto stackholderCreateDto)
    {
        var response = await _logic.Put(Id, stackholderCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }


    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<UserCreateDto> stackholderDtoPatch)
    {
        var response = await _logic.Patch(Id, stackholderDtoPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var user = await _logic.Delete(Id);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(UserPasswordDTO userPasswordDTO)
    {
        var response = await _logic.ResetPassword(userPasswordDTO);
        // Handle the result
        if (response.IsSuccess)
        {
           // Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);        
    }
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
    {
        var response = await _logic.ForgotPassword(forgotPasswordDTO);
        // Handle the result
        if (response.IsSuccess)
        {
            // Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPost("ResetForgotPassword")]
    public async Task<IActionResult> ResetForgotPassword(ResetPasswordDTO resetPasswordDTO)
    {
        var response = await _logic.ResetForgotPassword(resetPasswordDTO);
        // Handle the result
        if (response.IsSuccess)
        {
            // Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }
}
