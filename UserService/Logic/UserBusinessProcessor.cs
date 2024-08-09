using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MassTransit.Courier.Contracts;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using UserService.Commands;
using UserService.Dtos;

using UserService.Queries;

namespace UserService.Logic;
public class UserBusinessProcessor : IUserBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
    {
        var userList = await _sender.Send(new GetUsersQuery(page, pageSize));
        var userReadDtoList = _mapper.Map<List<UserReadDto>>(userList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = userReadDtoList,
        };
    }
    public async Task<ResponseDto> Get(Guid? id)
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
    public async Task<ResponseDto> Post(UserCreateDto request)
    {
        var user = _mapper.Map<User>(request);

        var existingUser = await _sender.Send(new GetUserByMobileNumberOrEmailQuery(user));
        if (existingUser != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_UserExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateUserCommand(user));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToCreateNewUser }
            };
        }

        var resultDto = _mapper.Map<UserReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.User_UserCreated
        };
    }
    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<UserCreateDto> request)
    {            
        if (request.Operations[0].path.ToString().ToLower() == "mobilenumber")
        {
            var user = _mapper.Map<User>(request);
            user.MobileNumber = request.Operations[0].value.ToString();
            var checkUser = await _sender.Send(new GetUserByMobileNumberOrEmailQuery(user));
            if (checkUser != null)
            {
                if (checkUser.Id != Id)
                {
                    return new ResponseDto()
                    {
                        IsSuccess = false,
                        Data = _mapper.Map<UserReadDto>(checkUser),
                        ErrorMessages = new List<string>() { AppConstants.User_UserExistsWithMobileOrEmail }
                    };
                }
            }
        }      

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
        return new ResponseDto()
        {
            IsSuccess = true,
            DisplayMessage = AppConstants.User_UserDelete,
            Data = userReadDto,
        };
    }

    public async Task<ResponseDto> ResetPassword(UserPasswordDTO userPasswordDTO)
    {
        //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
        // Write these validations in a seperate method
        return null;
    }

    public async Task<ResponseDto> Put(Guid? id, string? mobileNo, UserCreateDto request)
    {
        var users = _mapper.Map<User>(request);
        if (string.IsNullOrEmpty(mobileNo))
        {
            var existingSubscription = await _sender.Send(new GetUserByIdQuery(id));
            if (existingSubscription == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null, // No need to map existingSubscription if it's null
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }

            users.Id = (Guid)id; // Assigning the provided Id to the subscription
            var result = await _sender.Send(new PutUserCommand(mobileNo, users));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null, // No need to map result if it's null
                    ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
                };
            }
            var resultDto = _mapper.Map<UserReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.User_UserUpdated
            };
        }
        else
        {
            var result = await _sender.Send(new PutUserCommand(mobileNo,users));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null, // No need to map result if it's null
                    ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
                };
            }
            var resultDto = _mapper.Map<UserReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.User_UserUpdated
            };
        }
    }

    public async Task<ResponseDto> Get(int page = 1, int pageSize = 10, string link=null)
    {
        var user = await _sender.Send(new GetUserByLinkQuery(page, pageSize, link));
        if (user == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.User_UserNotFound }
            };
        }
        var userReadDto = _mapper.Map<List<UserReadDto>>(user);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = userReadDto,
        };
    }

    public async Task<ResponseDto> PostTempUser(TempUserCreateDto request)
    {
        var user = _mapper.Map<User>(request);
        var tempUser = _mapper.Map<TempUser>(request);

        var existingUser = await _sender.Send(new GetUserByMobileNumberOrEmailQuery(user));
        if (existingUser != null)
        {
            tempUser.Id = existingUser.Id;
            //return new ResponseDto()
            //{
            //    IsSuccess = false,
            //    Data = _mapper.Map<UserReadDto>(existingUser),
            //    ErrorMessages = new List<string>() { AppConstants.User_UserExistsWithMobileOrEmail }
            //};
        }

        var result = await _sender.Send(new CreateTempUserCommand(tempUser));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToCreateNewUser }
            };
        }

        var resultDto = _mapper.Map<TempUserReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.User_UserCreated
        };
    }
    public async Task<ResponseDto> GetUpdatedUser(string mobileNumber)
    {
        var user = await _sender.Send(new GetUserByMobileNumberQuery(mobileNumber));
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
}

