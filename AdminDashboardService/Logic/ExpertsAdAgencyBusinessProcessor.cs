using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;
using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AdminDashboardService.Commands;

namespace AdminDashboardService.Logic;
public class ExpertsAdAgencyBusinessProcessor : IExpertsAdAgencyBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ExpertsAdAgencyBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {        
            var expertsAdAgencyList = await _sender.Send(new GetExpertsAdAgencyQuery());
            var expertsAdAgencyReadDtoList = _mapper.Map<List<ExpertsAdAgencyReadDto>>(expertsAdAgencyList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsAdAgencyReadDtoList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var expertsAdAgency = await _sender.Send(new GetExpertsAdAgencyByIdQuery(id));
        if (expertsAdAgency == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
      //  var expertsAdAgencyReadDto = _mapper.Map<ExpertsAdAgencyDto>(expertsAdAgency);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = expertsAdAgency,
        };
    }
    /// <inheritdoc/>
   
    //public async Task<ExpertsReadDto> Delete(Guid Id)
    //{
    //    var user = await _sender.Send(new DeleteExpertsCommand(Id));
    //    var userReadDto = _mapper.Map<ExpertsReadDto>(user);
    //    return userReadDto;
    //}

    public async Task<ResponseDto> Delete(Guid id)
    {
        var expertsAdAgencyDetails = await _sender.Send(new DeleteExpertsAdAgencyCommand(id));
        var expertsAdAgencyReadDto = _mapper.Map<ResponseDto>(expertsAdAgencyDetails);
        return expertsAdAgencyReadDto;
    }

    public async Task<ResponseDto> Post(ExpertsAdAgencyCreateDto request)
    {
        var expertsAdAgency = _mapper.Map<ExpertsAdvertisingAgency>(request);

        var expertsAdAgencyIndividual = await _sender.Send(new GetExpertsAdAgencyByIdQuery(expertsAdAgency.Id));
        if (expertsAdAgencyIndividual != null || expertsAdAgencyIndividual.ToList().Count > 0)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertsAdAgencyReadDto>(expertsAdAgencyIndividual),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateExpertsAdAgencyCommand(expertsAdAgency));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertsAdAgencyReadDto>(expertsAdAgencyIndividual),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<ExpertsAdAgencyReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Put(Guid id, ExpertsAdAgencyCreateDto request)
    {
        var adagency = _mapper.Map<AdvertisingAgency>(request);

        var existingAdagency = await _sender.Send(new GetExpertsAdAgencyByIdQuery(id));
        if (existingAdagency == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertsAdAgencyReadDto>(existingAdagency),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        adagency.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutAdAgencyDetailsCommand(adagency));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertsAdAgencyReadDto>(existingAdagency),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<ExpertsAdAgencyReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsAdAgencyCreateDto> request)
    {
        var adagency = _mapper.Map<ExpertsAdvertisingAgency>(request);

        var existingAdagency = await _sender.Send(new GetExpertsAdAgencyByIdQuery(Id));
        if (existingAdagency == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        //var result = await _sender.Send(new PatchExpertsAdAgencyCommand(Id, request, existingAdagency));
        //if (result == null)
        //{
        //    return new ResponseDto()
        //    {
        //        IsSuccess = false,
        //        Data = _mapper.Map<ExpertsAdAgencyReadDto>(existingAdagency),
        //        ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
        //    };
        //}

        return new ResponseDto()
        {
            Data = _mapper.Map<ExpertsAdAgencyReadDto>(existingAdagency),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }
}

