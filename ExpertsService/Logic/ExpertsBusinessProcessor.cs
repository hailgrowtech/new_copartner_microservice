using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Commands;
using ExpertService.Dtos;
using ExpertService.Queries;
using MassTransit.Courier.Contracts;
using MigrationDB.Models;
using System.Web;
using System.Drawing.Printing;

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

    public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
    {        
            var expertsList = await _sender.Send(new GetExpertsQuery(page, pageSize));
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
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
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
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateExpertsCommand(experts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<ExpertReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }
    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsCreateDto> request)
    {
        var experts = _mapper.Map<Experts>(request);

        var existingExperts = await _sender.Send(new GetExpertsByIdQuery(Id));
        if (existingExperts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        var result = await _sender.Send(new PatchExpertsCommand(Id, request, existingExperts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<ExpertReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }
    //public async Task<ExpertsReadDto> Delete(Guid Id)
    //{
    //    var user = await _sender.Send(new DeleteExpertsCommand(Id));
    //    var userReadDto = _mapper.Map<ExpertsReadDto>(user);
    //    return userReadDto;
    //}

    public bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO)
    {
        //TODO : Encrypt Password. Make sure old and new password are not same. Make sure password is a combination of Alpha Numeric Char with Special Char and minmum 8 chars.
        // Write these validations in a seperate method
        return true;
    }
    public async Task<ResponseDto> Put(Guid id, ExpertsCreateDto request)
    {
        var experts = _mapper.Map<Experts>(request);

        var existingExperts = await _sender.Send(new GetExpertsByIdQuery(id));
        if (existingExperts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        experts.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutExpertsCommand(experts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ExpertReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<ExpertReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Delete(Guid id)
    {
        var expert = await _sender.Send(new DeleteExpertsCommand(id));
        var expertReadDto = _mapper.Map<ResponseDto>(expert);
        return expertReadDto;
    }


    public async Task<ResponseDto> GenerateReferralLink(Guid id)
    {

        // Assuming _sender.Send is an asynchronous method that returns an AffiliatePartner object
        var ra = await _sender.Send(new GetExpertsByIdQuery(id));

        // It's a good practice to use UriBuilder for constructing URLs to handle edge cases
        var uriBuilder = new UriBuilder("https://copartner.in/signup");
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["raid"] = id.ToString(); // Ensure the ID is converted to a string
        uriBuilder.Query = query.ToString();

        var referralLink = uriBuilder.ToString();

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = referralLink,
        };

    }

    public async Task<ResponseDto> GenerateExpertPaymentLink(Guid id)
    {

        // Assuming _sender.Send is an asynchronous method that returns an Expert object
        var ra = await _sender.Send(new GetExpertsByIdQuery(id));

        // Use UriBuilder to construct the URL with the GUID as part of the path
        var uriBuilder = new UriBuilder("https://copartner.in");
        uriBuilder.Path = $"/ra-detail/{id}";

        var referralLink = uriBuilder.ToString();

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = referralLink,
        };

    }


    public async Task<ResponseDto> GetListing(int page, int pageSize)
    {
        var expertsList = await _sender.Send(new GetExpertsQuery(page, pageSize));
        var expertsReadDtoList = _mapper.Map<List<ExpertReadDto>>(expertsList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = expertsReadDtoList,
        };
    }
    public async Task<ResponseDto> GetListingDetails(int page, int pageSize)
    {
        var expertsList = await _sender.Send(new GetExpertsQuery(page, pageSize));
        var expertsReadDtoList = _mapper.Map<List<ExpertReadDto>>(expertsList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = expertsReadDtoList,
        };
    }
}

