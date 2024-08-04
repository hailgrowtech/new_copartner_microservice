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
using Ocelot.Errors;
using static MassTransit.ValidationResultExtensions;

namespace FeaturesService.Logic;

public class ChatBusinessProcessor : IChatBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public ChatBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
    {
        var chatPlanList = await _sender.Send(new GetChatPlanQuery(page, pageSize));
        var chatPlanReadDtoList = _mapper.Map<List<ChatPlanReadDto>>(chatPlanList);
        var chatPlanReadDtoListWithDiscounts = chatPlanReadDtoList
            .Select(ToCalculateDiscountedAmount)
            .ToList();
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = chatPlanReadDtoListWithDiscounts,
        };

    }

    public async Task<ResponseDto> Get(Guid id)
    {
        var chatPlan = await _sender.Send(new GetChatPlanByIdQuery(id));
        if (chatPlan == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        var chatPlanReadDto = _mapper.Map<ChatPlanReadDto>(chatPlan);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = chatPlanReadDto,
        };
    }

    public async Task<ResponseDto> GetChatPlanByExpertsId(Guid id, int page , int pageSize)
    {
        var apListingData = await _sender.Send(new GetChatPlanByExpertsIdQuery(id, page, pageSize));
        if (apListingData == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        // var raListingReadDto = _mapper.Map<RAListingDataReadDto>(raListingData);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = apListingData,
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ChatPlanCreateDto> request)
    {
        var chatPlan = _mapper.Map<ChatPlan>(request);

        var existingChatPlan = await _sender.Send(new GetChatPlanByIdQuery(Id));
        if (existingChatPlan == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }

        var result = await _sender.Send(new PatchChatPlanCommand(Id, request, existingChatPlan));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ChatPlanReadDto>(existingChatPlan),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<ChatPlanReadDto>(result),
            DisplayMessage = AppConstants.Common_RecordUpdated
        };
    }
    public async Task<ResponseDto> Post(ChatPlanCreateDto request)
    {
        var chatPlan = _mapper.Map<ChatPlan>(request);

        var existingChatPlan = await _sender.Send(new GetChatPlanByIdQuery(chatPlan.Id));
        if (existingChatPlan != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ChatPlanReadDto>(existingChatPlan),
                ErrorMessages = new List<string>() { AppConstants.Common_AlreadyExistsRecord }
            };
        }

        var result = await _sender.Send(new CreateChatPlanCommand(chatPlan));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ChatPlanReadDto>(existingChatPlan),
                ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
            };
        }

        var resultDto = _mapper.Map<ChatPlanReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }

    public async Task<ResponseDto> Delete(Guid id)
    {
        var chatPlan = await _sender.Send(new DeleteChatPlanCommand(id));
        var expertReadDto = _mapper.Map<ResponseDto>(chatPlan);
        if (expertReadDto == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToDeleteRecord }
            };
        }
            return new ResponseDto()
            {
                Data = expertReadDto,
                DisplayMessage = AppConstants.Common_RecordDeleted
            };
    }
    public Task<ResponseDto> Put(Guid id, ChatPlanCreateDto chatPlanCreateDto)
    {
        throw new NotImplementedException();
    }
    public async Task<ResponseDto> PostChatUser(ChatUserCreateDto request)
    {
        var chatUser= _mapper.Map<ChatUser>(request);

        var existingChatUser = await _sender.Send(new GetChatUserByUsernameQuery(chatUser.Id));
        if (existingChatUser != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_AlreadyExistsRecord }
            };
        }

        var result = await _sender.Send(new CreateChatUserCommand(chatUser));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ChatUserReadDto>(existingChatUser),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<ChatUserReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }
    public async Task<ResponseDto> GetChatUserById(Guid id)
    {
        var chatUser = await _sender.Send(new GetChatMessageByIdQuery(id));
        if (chatUser == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
      //  var chatUserReadDto = _mapper.Map<ChatUserReadDto>(chatUser);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = chatUser,
        };
    }

    public async Task<ResponseDto> PostFreeChat(FreeChatCreateDto request)
    {
        var freeChat = _mapper.Map<FreeChat>(request);

        var existingFreeChatUser = await _sender.Send(new GetFreeChatByIdQuery(freeChat.UserId,freeChat.ExpertsId));
        if (existingFreeChatUser != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_AlreadyExistsRecord }
            };
        }

        var result = await _sender.Send(new CreateFreeChatCommand(freeChat));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<ChatUserReadDto>(existingFreeChatUser),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<FreeChatReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }

    public async Task<ResponseDto> GetFreeChatUser(Guid id)
    {
        try
        {
            var freeChat = await _sender.Send(new GetFreeChatByUserIdQuery(id));
            if (freeChat == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
          //  var freeChatReadDto = _mapper.Map<FreeChatReadDto>(freeChat);
            var freeChatReadDtoList = _mapper.Map<List<FreeChatReadDto>>(freeChat);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = freeChatReadDtoList,
            };
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    public ChatPlanReadDto ToCalculateDiscountedAmount(ChatPlanReadDto chatPlanReadDto)
    {
        if (chatPlanReadDto.Price.HasValue &&
            chatPlanReadDto.DiscountPercentage.HasValue &&
            chatPlanReadDto.DiscountValidFrom.HasValue &&
            chatPlanReadDto.DiscountValidTo.HasValue)
        {
            var currentDate = DateTime.Now;
            if (currentDate >= chatPlanReadDto.DiscountValidFrom.Value && currentDate <= chatPlanReadDto.DiscountValidTo.Value)
            {
                var discount = chatPlanReadDto.Price.Value * chatPlanReadDto.DiscountPercentage.Value / 100;
                chatPlanReadDto.DiscountedAmount = chatPlanReadDto.Price.Value - discount;
            }
            else if (currentDate >= chatPlanReadDto.DiscountValidTo.Value)
            {
                chatPlanReadDto.DiscountValidFrom = null;
                chatPlanReadDto.DiscountValidTo = null;
                chatPlanReadDto.DiscountPercentage = null;
                chatPlanReadDto.DiscountedAmount = chatPlanReadDto.Price.Value;
            }
        }
        else
        {
            chatPlanReadDto.DiscountValidFrom = null;
            chatPlanReadDto.DiscountValidTo = null;
            chatPlanReadDto.DiscountPercentage = null;
            // subscriptionReadDto.DiscountedAmount = null;
        }

        return chatPlanReadDto;
    }

}
