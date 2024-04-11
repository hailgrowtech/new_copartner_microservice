using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Dtos;
using SubscriptionService.Queries;

namespace SubscriptionService.Logic
{
    public class SubscriberProcessor : ISubscriberProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SubscriberProcessor(ISender sender, IMapper mapper)
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

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> request)
        {
            var subscribers = _mapper.Map<Subscriber>(request);

            var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(subscribers.Id));
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
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateSubscriberCommand(subscribers));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
                };
            }

            var resultDto = _mapper.Map<SubscriberReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
