using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface ISubscriberProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(SubscriberCreateDto subscriberCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> subscriberCreateDto);
    }
}
