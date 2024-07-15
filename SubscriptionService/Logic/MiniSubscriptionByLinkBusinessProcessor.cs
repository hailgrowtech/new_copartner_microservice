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
    public class MiniSubscriptionByLinkBusinessProcessor : IMiniSubscriptionByLinkBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public MiniSubscriptionByLinkBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }


        public async Task<ResponseDto> Delete(Guid id)
        {
            var minisubscription = await _sender.Send(new DeleteMiniSubscriptionLinkCommand(id));
            var minisubscriptionReadDto = _mapper.Map<ResponseDto>(minisubscription);
            return minisubscriptionReadDto;
        }

        public async Task<ResponseDto> Get()
        {
            var miniSubscriptionLinkMstList = await _sender.Send(new GetMiniSubscripionLinkQuery());
            var miniSubscriptionLinkMstReadDtoList = _mapper.Map<List<MiniSubscriptionReadDto>>(miniSubscriptionLinkMstList);
           
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = miniSubscriptionLinkMstReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var miniSubscriptionLinkMstList = await _sender.Send(new GetMiniSubscriptionLinkByIdQuery(id));
            if (miniSubscriptionLinkMstList == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }

            var miniSubscriptionLinkMstReadDtoList = _mapper.Map<MiniSubscriptionReadDto>(miniSubscriptionLinkMstList);

            return new ResponseDto()
            {
                IsSuccess = true,
                Data = miniSubscriptionLinkMstReadDtoList,
            };
        }

        public async Task<ResponseDto> Post(MiniSubscriptionCreateDto request)
        {
            var miniSubscription = _mapper.Map<MinisubscriptionLink>(request);

            var existingsubscription = await _sender.Send(new GetMiniSubscriptionLinkByIdQuery(miniSubscription.Id));
            if (existingsubscription != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<MiniSubscriptionReadDto>(existingsubscription),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateMiniSubscriptionLinkCommand(request.ExpertId, request.SubscriptionId));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<MiniSubscriptionReadDto>(existingsubscription),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<MiniSubscriptionReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
