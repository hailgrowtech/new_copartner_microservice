using CommonLibrary.CommonDTOs;
using Copartner;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface ISubscriberBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(int page, int pageSize, string link);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(SubscriberCreateDto subscriberCreateDto);
        Task<ResponseDto> Put(Guid id, SubscriberCreateDto subscriberCreateDto);

        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> subscriberCreateDto);
        Task <WalletEvent>  ProcessSubscriberWallet(Guid subscriberId);

        Task<ResponseDto> GetByUserId(Guid id);
        Task<ResponseDto> PostTempSubscription(SubscriberCreateDto subscriberCreateDto);
        Task<ResponseDto> ProcessTempSubscription(Guid userId, Guid subscriberId);

    }
}
