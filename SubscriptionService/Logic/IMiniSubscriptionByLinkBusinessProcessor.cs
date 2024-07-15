using CommonLibrary.CommonDTOs;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic
{
    public interface IMiniSubscriptionByLinkBusinessProcessor
    {
        Task<ResponseDto> Get();
        //Task<ResponseDto> Get(int page, int pageSize, string link);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(MiniSubscriptionCreateDto subscriberCreateDto);
        //Task<ResponseDto> Put(Guid id, SubscriberCreateDto subscriberCreateDto);

        Task<ResponseDto> Delete(Guid id);
    }
}
