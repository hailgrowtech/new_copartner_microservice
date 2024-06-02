using AuthenticationService.Models;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MassTransit.Courier.Contracts;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AuthenticationService.Commands;
using AuthenticationService.Dtos;

using AuthenticationService.Queries;
using AuthenticationService.Profiles;
using MassTransit.Mediator;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace AuthenticationService.Logic;
public class UserBusinessProcessor : IUserBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly AuthMapperProfile _authMapper;
    public UserBusinessProcessor(ISender sender, IMapper mapper,AuthMapperProfile authMapper)
    {
        this._sender = sender;
        this._mapper = mapper;
        this._authMapper = authMapper;
    }

    public async Task<ResponseDto> Get(string userType, int page = 1, int pageSize = 10)
    {
        var userList = await _sender.Send(new GetUserQuery(userType, page, pageSize));
        var userReadDtoList = _mapper.Map<List<UserReadDto>>(userList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = userReadDtoList,
        };
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var user = await _sender.Send(new GetUserByIdQuery(id));
        if (user == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.User_UserNotFound }
            };
        }
        var userReadDto = _mapper.Map<UserReadDto>(user);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = userReadDto,
        };
    }
    /// <inheritdoc/>
    /// 
    public async Task<bool> SaveUserAuthDetails(AuthenticationDetail userAuthDetails)
    {
        //  var userobj = _mapper.Map<User>(user);
        var resutl = await _sender.Send(new CreateUserAuthDetailsCommand(userAuthDetails));
        return true;
    }
    public async Task<bool> SaveUserAuth(Authentication userAuth)
    {
        //  var userobj = _mapper.Map<User>(user);
        var resutl = await _sender.Send(new CreateUserAuthCommand(userAuth));
        return true;
    }
    public async Task<ResponseDto> Post(UserCreateDto request)
    {
        var user = _mapper.Map<AuthenticationDetail>(request);

        var existingUser = await _sender.Send(new GetUserByEmailQuery(user));
        if (existingUser != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.Common_AlreadyExistsRecord }
            };
        }

        // Map AuthenticationRequestDTO to UserCreatedEventDTO
        var userCreatedEventDto = new UserCreateDto
        {
            // Assuming AuthenticationRequestDTO contains these properties
            UserType = request.UserType,
            StackholderId = request.StackholderId,
            UserId = Guid.NewGuid(),
            Name = request.Name,
            MobileNo = request.MobileNo,
            Email = request.Email,
            Password = request.Password,
            IsActive = request.IsActive,
        };
        var authDetailEntity = _authMapper.ToCreateAuthDetailEntity(userCreatedEventDto);//.ToCreateAuthDetailEntity();
        var result = await _sender.Send(new CreateUserAuthDetailsCommand(authDetailEntity));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToCreateNewUser }
            };
        }
        var authEntity = _authMapper.ToCreateAuthEntity(userCreatedEventDto);//.ToCreateAuthDetailEntity();
        var responseAuth =  await _sender.Send(new CreateUserAuthCommand(authEntity));

        var resultDto = _mapper.Map<UserReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.User_UserCreated
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<UserCreateDto> request)
    {
        var user = _mapper.Map<AuthenticationDetail>(request);

        var existingUser = await _sender.Send(new GetUserByIdQuery(Id));
        if (existingUser == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_UserNotFound }
            };
        }

        var result = await _sender.Send(new PatchUserCommand(Id, request, existingUser));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<UserReadDto>(result),
            DisplayMessage = AppConstants.User_UserUpdated
        };
    }
    public async Task<ResponseDto> Delete(Guid Id)
    {
        var user = await _sender.Send(new DeleteUserCommand(Id));
        var userReadDto = _mapper.Map<ResponseDto>(user);
        return userReadDto;
    }

   

    public async Task<ResponseDto> Put(Guid id, UserCreateDto request)
    {
        var users = _mapper.Map<AuthenticationDetail>(request);

        var existingSubscription = await _sender.Send(new GetUserByIdQuery(id));
        if (existingSubscription == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingSubscription),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        users.Id = id; // Assigning the provided Id to the subscription
        var result = await _sender.Send(new PutUserCommand(users));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingSubscription),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<UserReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }
    public async Task<ResponseDto> ResetPassword(UserPasswordDTO request)
    {
        //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
        // Write these validations in a seperate method
        // Map request to command
        // Validate password
        var validationResult = ValidatePassword(request);
        if (!validationResult.IsValid)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = validationResult.ErrorMessages
            };
        }

        // Send the command
        var result = await _sender.Send(new ResetUserAuthCommand(request));
        if (!result)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { "Failed to reset password." }
            };
        }

        return new ResponseDto()
        {
            IsSuccess = true,
            DisplayMessage = "Password reset successfully."
        };
    }
    public async Task<ResponseDto> ForgotPassword(ForgotPasswordDTO request)
    {
        var forgotPassword = _mapper.Map<ForgotPassword>(request);
        //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
        // Write these validations in a seperate method
        // Map request to command
        // check email
        var user = new AuthenticationDetail
        {
            Id = new Guid(),
            UserType = string.Empty,
            StackholderId = new Guid(),
            UserId = new Guid(),
            Name = string.Empty,
            Email = request.Email,
            MobileNumber = string.Empty,
            PasswordHash = string.Empty,
            IsActive = true,
        };
  
        var existingUser = await _sender.Send(new GetUserByEmailQuery(user));
        if (existingUser == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        forgotPassword.UserId = existingUser.UserId;
        forgotPassword.Token= TokenGenerator.GenerateToken();
        forgotPassword.Expires = DateTime.UtcNow.AddHours(1);
        forgotPassword.IsValidated = false;

        // Send the command
        var result = await _sender.Send(new ForgotPasswordCommand(forgotPassword));
        if (!result)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { "Failed to reset password." }
            };
        }

        return new ResponseDto()
        {
            IsSuccess = true,
            DisplayMessage = "Password reset successfully."
        };
    }
    private ValidationResult ValidatePassword(UserPasswordDTO request)
    {
        var validationErrors = new List<string>();

        // Check if old and new passwords are the same
        if (request.OldPassword == request.NewPassword)
        {
            validationErrors.Add("Old and new passwords cannot be the same.");
        }

        // Implement password strength validation
        if (!IsStrongPassword(request.NewPassword))
        {
            validationErrors.Add("Password must be at least 8 characters long and contain a combination of alphanumeric characters and special characters.");
        }

        // Return ValidationResult object with error messages
        return new ValidationResult
        {
            IsValid = validationErrors.Count == 0,
            ErrorMessages = validationErrors
        };
    }



    private bool IsStrongPassword(string password)
    {
        // Implement password strength validation logic here
        // For example, using regular expressions
        string pattern = @"^(?=.*[a-zA-Z0-9])(?=.*[^a-zA-Z0-9\s]).{8,}$";
        return Regex.IsMatch(password, pattern);
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; }

    public ValidationResult()
    {
        IsValid = true;
        ErrorMessages = new List<string>();
    }
}

public static class TokenGenerator
{
    public static string GenerateToken(int length = 32)
    {
        byte[] tokenData = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenData);
        }
        return Convert.ToBase64String(tokenData);
    }
}