using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Commands;
using ExpertService.Dtos;
using ExpertService.Models;
using ExpertService.Queries;

namespace ExpertService.Logic;
public class ExpertsBusinessProcessor : IExpertsBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ExpertsBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {        
            var expertsList = await _sender.Send(new GetExpertsQuery());
            var expertsReadDtoList = _mapper.Map<List<ExpertReadDto>>(expertsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsReadDtoList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var experts = await _sender.Send(new GetExpertsByIdQuery(id));
        if (experts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.User_UserNotFound }
            };
        }
        var expertsReadDto = _mapper.Map<ExpertReadDto>(experts);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = expertsReadDto,
        };
    }
    /// <inheritdoc/>
    public async Task<ResponseDto> Post(ExpertsCreateDto request)
    {
        var experts = _mapper.Map<Experts>(request);

        var existingExperts = await _sender.Send(new GetExpertsByMobileNumberOrEmailQuery(experts));
        if (existingExperts != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.User_UserExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateExpertsCommand(experts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToCreateNewUser }
            };
        }

        var resultDto = _mapper.Map<ExpertReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.User_UserCreated
        };
    }
    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsCreateDto> request)
    {
        var experts = _mapper.Map<Experts>(request);

        var existingExperts = await _sender.Send(new GetExpertsByIdQuery(experts.Id));
        if (existingExperts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<UserReadDto>(existingUser),
                ErrorMessages = new List<string>() { AppConstants.User_UserNotFound }
            };
        }

        var result = await _sender.Send(new PatchExpertsCommand(Id, request, existingExperts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<ExpertReadDto>(result),
            DisplayMessage = AppConstants.User_UserUpdated
        };
    }
    //public async Task<UserReadDto> Delete(Guid Id)
    //{
    //    var user = await _sender.Send(new DeleteUserCommand(Id));
    //    var userReadDto = _mapper.Map<UserReadDto>(user);
    //    return userReadDto;
    //}

    public bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO)
    {
        //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
        // Write these validations in a seperate method
        return true;
    }
}

