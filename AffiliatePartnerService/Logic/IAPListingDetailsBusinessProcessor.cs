using AffiliatePartnerService.Dtos;
using CommonLibrary.CommonDTOs;

namespace AffiliatePartnerService.Logic
{
    public interface IAPListingDetailsBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Put(Guid id, APListingDetailDto aPListingDetailDto);
    }
}
