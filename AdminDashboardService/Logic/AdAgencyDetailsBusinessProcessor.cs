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
public class AdAgencyDetailsBusinessProcessor : IAdAgencyDetailsBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AdAgencyDetailsBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {        
            var adAgencyDetailsList = await _sender.Send(new GetAdAgencyDetailsQuery());
            //var adAgencyDetailsReadDtoList = _mapper.Map<List<AdAgencyDetailsReadDto>>(adAgencyDetailsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = adAgencyDetailsList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var adAgencyDetails = await _sender.Send(new GetAdAgencyDetailsByIdQuery(id));
        if (adAgencyDetails == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
       // var adAgencyDetailsReadDto = _mapper.Map<AdAgencyDetailsReadDto>(adAgencyDetails);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = adAgencyDetails,
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
        var adAgencyDetails = await _sender.Send(new DeleteAdAgencyDetailsCommand(id));
        var adAgencyDetailsReadDto = _mapper.Map<ResponseDto>(adAgencyDetails);
        return adAgencyDetailsReadDto;
    }

    public async Task<ResponseDto> Post(AdAgencyDetailsCreateDto request)
    {
        var adAgency = _mapper.Map<AdvertisingAgency>(request);

        var adAgencyIndividual = await _sender.Send(new GetAdAgencyDetailsByIdQuery(adAgency.Id));
        if (adAgencyIndividual != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<AdAgencyDetailsReadDto>(adAgencyIndividual),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateAdAgencyDetailsCommand(adAgency));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<AdAgencyDetailsReadDto>(adAgencyIndividual),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<AdAgencyDetailsReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Put(Guid id, AdAgencyDetailsCreateDto request)
    {
        var adagency = _mapper.Map<AdvertisingAgency>(request);

        var existingAdagency = await _sender.Send(new GetAdAgencyDetailsByIdQuery(id));
        if (existingAdagency == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<AdAgencyDetailsReadDto>(existingAdagency),
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
                Data = _mapper.Map<AdAgencyDetailsReadDto>(existingAdagency),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<AdAgencyDetailsReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> request)
    {
        //var adagency = _mapper.Map<AdvertisingAgency>(request);

        //var existingAdagency = await _sender.Send(new GetAdAgencyDetailsByIdQuery(Id));
        //if (existingAdagency == null)
        //{
        //    return new ResponseDto()
        //    {
        //        IsSuccess = false,
        //        ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
        //    };
        //}

        //var result = await _sender.Send(new PatchAdAgencyDetailsCommand(Id, request, existingAdagency));
        //if (result == null)
        //{
        //    return new ResponseDto()
        //    {
        //        IsSuccess = false,
        //        Data = _mapper.Map<AdAgencyDetailsReadDto>(existingAdagency),
        //        ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
        //    };
        //}

        return new ResponseDto()
        {
            Data = null,//_mapper.Map<AdAgencyDetailsReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }
}

