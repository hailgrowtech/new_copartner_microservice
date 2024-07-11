using AgoraIO.Media;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using FeaturesService.Commands;
using FeaturesService.Dtos;
using FeaturesService.Queries;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace FeaturesService.Logic;

public class WebinarBookingBusinessProcessor : IWebinarBookingBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    private const string AppId = "694e36f5e4da4440b9d3df6a711efc26";
    private const string AppCertificate = "b8bc4bc937564bb2966f5a1df8e1fd26";
    public WebinarBookingBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
    {
        var webinarBookingList = await _sender.Send(new GetWebinarBookingQuery(page, pageSize));
        var webinarBookingReadDtoList = _mapper.Map<List<WebinarBookingReadDto>>(webinarBookingList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = webinarBookingReadDtoList,
        };
    }

    public async Task<ResponseDto> Get(Guid id)
    {
        var webinarBooking = await _sender.Send(new GetWebinarBookingByIdQuery(id));
        if (webinarBooking == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var webinarBookingReadDto = _mapper.Map<WebinarBookingReadDto>(webinarBooking);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = webinarBookingReadDto,
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<WebinarBookingCreateDto> request)
    {
        var webinar = _mapper.Map<WebinarBooking>(request);

        var existingwebinars = await _sender.Send(new GetWebinarBookingByIdQuery(Id));
        if (existingwebinars == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        var result = await _sender.Send(new PatchWebinarBookingCommand(Id, request, existingwebinars));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WebinarBookingReadDto>(existingwebinars),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<WebinarBookingReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }

    public async Task<ResponseDto> Post(WebinarBookingCreateDto request)
    {
        var webinar = _mapper.Map<WebinarBooking>(request);

        var existingwebinars = await _sender.Send(new GetWebinarBookingByIdQuery(webinar.Id));
        if (existingwebinars != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WebinarBookingReadDto>(existingwebinars),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateWebinarBookingCommand(webinar));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WebinarBookingReadDto>(existingwebinars),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<WebinarBookingReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public Task<ResponseDto> Put(Guid id, WebinarBookingCreateDto webinarBookingCreateDto)
    {
        throw new NotImplementedException();
    }

    public string GenerateToken(string channelName, string uid)
    {
        var expirationTimeInSeconds = 3600;
        var currentTimestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        var privilegeExpiredTs = currentTimestamp + expirationTimeInSeconds;

        var token = new AccessToken(AppId, AppCertificate, channelName, uid);
        token.addPrivilege(Privileges.kJoinChannel, (uint)privilegeExpiredTs);
        return token.build();

    }
}
