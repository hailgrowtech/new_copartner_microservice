using CommonLibrary.CommonDTOs;

namespace AdminDashboardService.Logic
{
    public interface IUserDataListingBusinessProcessor
    {
        Task<ResponseDto> GetUserListing();
        Task<ResponseDto> GetFirstTimePaymentListing();
        Task<ResponseDto> GetSecondTimePaymentListing();
    }
}
