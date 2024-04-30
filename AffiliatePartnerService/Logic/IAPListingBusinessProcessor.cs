using AffiliatePartnerService.Dtos;
using CommonLibrary.CommonDTOs;

namespace AffiliatePartnerService.Logic
{
    public interface IAPListingBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Put(Guid id, APListingDto aPListingDto);

    }
}
