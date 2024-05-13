using AffiliatePartnerService.Dtos;
using CommonLibrary.CommonDTOs;

namespace AffiliatePartnerService.Logic
{
    public interface IAPListingBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10);
        Task<ResponseDto> Put(Guid id, APListingDto aPListingDto);

    }
}
