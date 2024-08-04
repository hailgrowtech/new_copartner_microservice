using CommonLibrary.CommonDTOs;
using FeaturesService.Dtos;
using FeaturesService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace FeaturesService.Logic;
public interface IChatBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> GetChatPlanByExpertsId(Guid id, int page = 1, int pageSize = 10);
    Task<ResponseDto> Post(ChatPlanCreateDto chatPlanCreateDto);
    Task<ResponseDto> Put(Guid id, ChatPlanCreateDto chatPlanCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ChatPlanCreateDto> chatPlanCreateDto);
    Task<ResponseDto> Delete(Guid id);
    Task<ResponseDto> PostChatUser(ChatUserCreateDto chatUserCreateDto);
    Task<ResponseDto> GetChatUserById(Guid id);
    Task<ResponseDto> PostFreeChat(FreeChatCreateDto freeChatCreateDto);
    Task<ResponseDto> GetFreeChatUser(Guid id);
}
