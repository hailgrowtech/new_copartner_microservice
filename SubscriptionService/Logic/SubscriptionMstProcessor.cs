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
    public class SubscriptionMstProcessor : ISubscriptionMstProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SubscriptionMstProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var subscriptionMstList = await _sender.Send(new GetSubscriptionMstQuery());
            var subscriptionMstReadDtoList = _mapper.Map<List<SubscriptionMstReadDto>>(subscriptionMstList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscriptionMstReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var subscriptionMsts = await _sender.Send(new GetSubscriptionMstByIdQuery(id));
            if (subscriptionMsts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var subscriptionMstsReadDto = _mapper.Map<SubscriptionMstReadDto>(subscriptionMsts);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscriptionMsts,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriptionMstCreateDto> request)
        {
            var subscriptionMsts = _mapper.Map<SubscriptionMst>(request);

            var existingsubscriptionMsts = await _sender.Send(new GetSubscriptionMstByIdQuery(subscriptionMsts.Id));
            if (existingsubscriptionMsts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchSubscriptionMstCommand(Id, request, existingsubscriptionMsts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionMstReadDto>(existingsubscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<SubscriptionMstReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(SubscriptionMstCreateDto request)
        {
            var subscriptionMsts = _mapper.Map<SubscriptionMst>(request);

            var existingsubscriptionMsts = await _sender.Send(new GetSubscriptionMstByIdQuery(subscriptionMsts.Id));
            if (existingsubscriptionMsts != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionMstReadDto>(existingsubscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateSubscriptionMstCommand(subscriptionMsts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionMstReadDto>(existingsubscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
                };
            }

            var resultDto = _mapper.Map<SubscriptionMstReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
