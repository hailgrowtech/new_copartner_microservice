using AdminDashboardService.Commands;
using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using System.Drawing.Printing;

namespace AdminDashboardService.Logic
{
    public class TelegramMessageBusinessProcessor : ITelegramMessageBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TelegramMessageBusinessProcessor(ISender sender, IMapper mapper)
        {
            this._sender = sender;
            this._mapper = mapper;
        }

        public Task<ResponseDto> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var telegramMessageList = await _sender.Send(new GetTelegramMessageQuery(page, pageSize));
            var telegramMessageReadDtoList = _mapper.Map<List<TelegramMessageReadDto>>(telegramMessageList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = telegramMessageReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id, string userType, int page = 1, int pageSize = 10)
        {
            var telegramMessage = await _sender.Send(new GetTelegramMessageByIdAPRAQuery(id,userType,page,pageSize));
            if (telegramMessage == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var telegramMessageReadDtos = _mapper.Map<IEnumerable<TelegramMessageReadDto>>(telegramMessage);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = telegramMessageReadDtos,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {

            var telegramMessage = await _sender.Send(new GetTelegramMessageByIdQuery(id));
            if (telegramMessage == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var telegramMessageReadDto = _mapper.Map<TelegramMessageReadDto>(telegramMessage);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = telegramMessageReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<TelegramMessageCreateDto> request)
        {
            var blogs = _mapper.Map<TelegramMessage>(request);

            var existingTelegramMessage = await _sender.Send(new GetTelegramMessageByIdQuery(Id));
            if (existingTelegramMessage == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchTelegramMessageCommand(Id, request, existingTelegramMessage));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<TelegramMessageReadDto>(existingTelegramMessage),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<TelegramMessageReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(TelegramMessageCreateDto request)
        {
            var telegramMessage = _mapper.Map<TelegramMessage>(request);

            var existingTelegramMessage = await _sender.Send(new GetTelegramMessageByIdQuery(telegramMessage.Id));
            if (existingTelegramMessage != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<TelegramMessageReadDto>(existingTelegramMessage),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }

            var result = await _sender.Send(new CreateTelegramMessageCommand(telegramMessage));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<TelegramMessageReadDto>(existingTelegramMessage),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<TelegramMessageReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public Task<ResponseDto> Put(Guid id, TelegramMessageCreateDto telegramMessageCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
