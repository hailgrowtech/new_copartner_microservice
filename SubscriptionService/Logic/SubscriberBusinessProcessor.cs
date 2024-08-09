using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using Copartner;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Dtos;
using SubscriptionService.Queries;

namespace SubscriptionService.Logic;

public class SubscriberBusinessProcessor : ISubscriberBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SubscriberBusinessProcessor(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {
        var subscriberList = await _sender.Send(new GetSubscriberQuery());
        var subscriberReadDtoList = _mapper.Map<List<SubscriberReadDto>>(subscriberList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = subscriberReadDtoList,
        };
    }

    public async Task<ResponseDto> Get(Guid id)
    {
        var subscribers = await _sender.Send(new GetSubscriberIdQuery(id));
        if (subscribers == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var subscriptionMstsReadDto = _mapper.Map<SubscriberReadDto>(subscribers);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = subscribers,
        };
    }
    public async Task<ResponseDto> GetByUserId(Guid id)
    {
        var subscribers = await _sender.Send(new GetSubscriberByUserIdQuery(id));
        if (subscribers == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
       // var subscriptionMstsReadDto = _mapper.Map<SubscriberReadDto>(subscribers);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = subscribers,
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> request)
    {
        var subscribers = _mapper.Map<Subscriber>(request);

        var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(Id));
        if (existingsubscribers == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        var result = await _sender.Send(new PatchSubscriberCommand(Id, request, existingsubscribers));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<SubscriberReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }

    public async Task<ResponseDto> Post(SubscriberCreateDto request)
    {
        var subscribers = _mapper.Map<Subscriber>(request);

        var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(subscribers.Id));
        if (existingsubscribers != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                ErrorMessages = new List<string>() { AppConstants.Subscriber_SubscriberNotFound }
            };
        }

        var result = await _sender.Send(new CreateSubscriberCommand(subscribers));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                ErrorMessages = new List<string>() { AppConstants.Subscriber_FailedToCreateNewSubscriber }
            };
        }

        var resultDto = _mapper.Map<SubscriberReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Subscriber_SubscriberCreated
        };
    }
    public async Task<ResponseDto> Delete(Guid Id)
    {
        var user = await _sender.Send(new DeleteSubscriptionCommand(Id));
        var userReadDto = _mapper.Map<ResponseDto>(user);
        return userReadDto;
    }

    public async Task<ResponseDto> Put(Guid id, SubscriberCreateDto request)
    {
        var subscriber = _mapper.Map<Subscriber>(request);

        var existingSubscriber = await _sender.Send(new GetSubscriberIdQuery(id));
        if (existingSubscriber == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingSubscriber),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        subscriber.Id = id; // Assigning the provided Id to the subscription
        var result = await _sender.Send(new PutSubscriberCommand(subscriber));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingSubscriber),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<SubscriberReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<WalletEvent> ProcessSubscriberWallet(Guid subscriberId)
    {
       // var wallet = _mapper.Map<WalletEvent>(request);
        var result = await _sender.Send(new GetSubscriberWalletQuery(subscriberId));
        if (result == null)
        {
            return null;
        }

        return result;
    }

    public async Task<ResponseDto> Get(int page, int pageSize, string link)
    {
        var subscribers = await _sender.Send(new GetSubscriberByLinkQuery(page, pageSize, link));
        if (subscribers == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var subscriptionMstsReadDto = _mapper.Map<List<SubscriberReadDto>>(subscribers);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = subscriptionMstsReadDto,
        };
    }

    public async Task<ResponseDto> PostTempSubscription(SubscriberCreateDto request)
    {
        var subscribers = _mapper.Map<Subscriber>(request);
        var tempSubscribers =_mapper.Map<TempSubscriber>(request);

        var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(subscribers.Id));
        if (existingsubscribers != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                ErrorMessages = new List<string>() { AppConstants.Subscriber_SubscriberNotFound }
            };
        }

        var result = await _sender.Send(new CreateTempSubscriberCommand(tempSubscribers));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                ErrorMessages = new List<string>() { AppConstants.Subscriber_FailedToCreateNewSubscriber }
            };
        }

        var resultDto = _mapper.Map<SubscriberReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Subscriber_SubscriberCreated
        };
    }

    public async Task<ResponseDto> ProcessTempSubscription(Guid userId, Guid subscriberId)
    {
        var subscribers = await _sender.Send(new CreateSubscriberProcessCommand(userId, subscriberId));
        if (subscribers == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var subscriptionMstsReadDto = _mapper.Map<SubscriberReadDto>(subscribers);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = subscriptionMstsReadDto,
        };
    }
}
