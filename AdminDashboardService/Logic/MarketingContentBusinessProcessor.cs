using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using AdminDashboardService.Commands;
using AdminDashboardService.Dtos;
using AdminDashboardService.Logic;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AdminDashboardService.Logic;
public class MarketingContentBusinessProcessor : IMarketingContentBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public MarketingContentBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {        
            var marketingContentList = await _sender.Send(new GetMarketingContentQuery());
            var marketingContentReadDtoList = _mapper.Map<List<MarketingContentReadDto>>(marketingContentList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = marketingContentReadDtoList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var marketingContent = await _sender.Send(new GetMarketingContentByIdQuery(id));
        if (marketingContent == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var marketingContentReadDto = _mapper.Map<MarketingContentReadDto>(marketingContent);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = marketingContentReadDto,
        };
    }
    /// <inheritdoc/>
    public async Task<ResponseDto> Post(MarketingContentCreateDto request)
    {
        var marketingContent = _mapper.Map<MarketingContent>(request);

        var existingmarketingContent = await _sender.Send(new GetMarketingContentByIdQuery(marketingContent.Id));
        if (existingmarketingContent != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<MarketingContentReadDto>(existingmarketingContent),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateMarketingServiceCommand(marketingContent));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<MarketingContentReadDto>(existingmarketingContent),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<MarketingContentReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }
    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<MarketingContentCreateDto> request)
    {
        var marketingContent = _mapper.Map<MarketingContent>(request);

        var existingmarketingContent = await _sender.Send(new GetMarketingContentByIdQuery(Id));
        if (existingmarketingContent == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        var result = await _sender.Send(new PatchMarketingServiceCommand(Id, request, existingmarketingContent));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<MarketingContentReadDto>(existingmarketingContent),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<MarketingContentReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }

    public async Task<ResponseDto> Put(Guid id, MarketingContentCreateDto request)
    {
        var marketingContent = _mapper.Map<MarketingContent>(request);

        var existingmarketingContent = await _sender.Send(new GetMarketingContentByIdQuery(id));
        if (existingmarketingContent == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<MarketingContentReadDto>(existingmarketingContent),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        marketingContent.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutMarketingServiceCommand(marketingContent));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<MarketingContentReadDto>(existingmarketingContent),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<MarketingContentReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Delete(Guid id)
    {
        var marketingContent = await _sender.Send(new DeleteMarketingServiceCommand(id));
        var marketingContentReadDto = _mapper.Map<ResponseDto>(marketingContent);
        return marketingContentReadDto;
    }
}

