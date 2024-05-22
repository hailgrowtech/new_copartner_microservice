using CommonLibrary.CommonDTOs;

namespace AdminDashboardService.Logic
{
    public interface IUserDataListingBusinessProcessor
    {
        Task<ResponseDto> GetUserListing(int page, int pageSize);
        Task<ResponseDto> GetFirstTimePaymentListing(int page, int pageSize);
        Task<ResponseDto> GetSecondTimePaymentListing(int page, int pageSize);
    }
}
