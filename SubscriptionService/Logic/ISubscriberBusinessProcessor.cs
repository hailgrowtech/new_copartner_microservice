using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface ISubscriberBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(SubscriberCreateDto subscriberCreateDto);
        Task<ResponseDto> Put(Guid id, SubscriberCreateDto subscriberCreateDto);

        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> subscriberCreateDto);
        void ProcessSubscriberWallet(Guid subscriberId);

        Task<ResponseDto> GetByUserId(Guid id);
    }
}
