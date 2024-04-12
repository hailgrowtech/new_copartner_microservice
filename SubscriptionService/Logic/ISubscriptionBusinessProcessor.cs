using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface ISubscriptionBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(SubscriptionCreateDto subscriptionMstCreateDto);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriptionCreateDto> subscriptionMstCreateDto);
    }
}
