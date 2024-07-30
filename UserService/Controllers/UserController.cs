﻿using CommonLibrary.Authorization;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Logic;

namespace UserService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserBusinessProcessor _logic;
    private readonly ILogger<UserController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public UserController(IUserBusinessProcessor logic, ILogger<UserController> logger,IMediator mediator)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
    {
        this._logic = logic;
        this._logger = logger;

       // this._topicProducer = topicProducer;
    }
    /// <summary>
    /// Gets the list of all Users.
    /// </summary>
    /// <returns>The list of Users.</returns>
    // GET: api/User
    [HttpGet]
    public async Task<object> Get(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching User Data..");
        var users = await _logic.Get(page, pageSize);
        return Ok(users);
    }

    [HttpGet("GetUserByLink", Name = "GetUserByLink")]
    public async Task<object> Get(int page = 1, int pageSize = 10, string link = null)
    {
        _logger.LogInformation("Fetching User Data..");
        var users = await _logic.Get(page, pageSize, link);
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
    [HttpGet("{Id}", Name = "Get")]
    public async Task<ActionResult<UserReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching user details for Id : " + Id.ToString());
        var user = await _logic.Get(Id);
        return  user != null ? (ActionResult<UserReadDto>)Ok(user) : NotFound();
    }


    [HttpPost]
    public async Task<object> Post(UserCreateDto userDto)
    {
        var response = await _logic.Post(userDto);

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

    [HttpPut]
    public async Task<object> Put(Guid? Id,string?MobileNo, UserCreateDto userCreateDto)
    {
        var response = await _logic.Put(Id,MobileNo, userCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }


    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<UserCreateDto> userDtoPatch)
    {        
        var response = await _logic.Patch(Id, userDtoPatch);
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
        var user = await _logic.Delete(Id);
        return user != null ? Ok(user) : NotFound();
    }
    //[HttpPut("{Id:guid}",Name ="ResetPassword")]
    //public async Task<object> ResetPassword(UserPasswordDTO userPasswordDTO)
    //{
    //    //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
    //    // Write these validations in a seperate method
    //    return true;
    //}
}
