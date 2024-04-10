using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface ISubscriptionMstProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(SubscriptionMstCreateDto subscriptionMstCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriptionMstCreateDto> subscriptionMstCreateDto);
    }
}
