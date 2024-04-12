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
    public class SubscriptionBusinessProcessor : ISubscriptionBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SubscriptionBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var subscriptionMstList = await _sender.Send(new GetSubscriptionQuery());
            var subscriptionMstReadDtoList = _mapper.Map<List<SubscriptionReadDto>>(subscriptionMstList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscriptionMstReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var subscriptionMsts = await _sender.Send(new GetSubscriptionByIdQuery(id));
            if (subscriptionMsts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var subscriptionMstsReadDto = _mapper.Map<SubscriptionReadDto>(subscriptionMsts);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscriptionMsts,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriptionCreateDto> request)
        {           
            var existingsubscriptionMsts = await _sender.Send(new GetSubscriptionByIdQuery(Id));
            if (existingsubscriptionMsts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var subscriptionMsts = _mapper.Map<SubscriptionMst>(existingsubscriptionMsts);
            var result = await _sender.Send(new PatchSubscriptionMstCommand(Id, request, subscriptionMsts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionReadDto>(subscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<SubscriptionReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(SubscriptionCreateDto request)
        {
            var subscriptionMsts = _mapper.Map<SubscriptionMst>(request);

            var existingsubscriptionMsts = await _sender.Send(new GetSubscriptionByIdQuery(subscriptionMsts.Id));
            if (existingsubscriptionMsts != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionReadDto>(existingsubscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateSubscriptionCommand(subscriptionMsts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriptionReadDto>(existingsubscriptionMsts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
                };
            }

            var resultDto = _mapper.Map<SubscriptionReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public async Task<ResponseDto> Delete(Guid Id)
        {
            var user = await _sender.Send(new DeleteSubscriptionCommand(Id));
            var userReadDto = _mapper.Map<ResponseDto>(user);
            return userReadDto;
        }
    }
}
