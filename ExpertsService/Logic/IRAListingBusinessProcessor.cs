using CommonLibrary.CommonDTOs;
using ExpertsService.Dtos;

namespace ExpertsService.Logic
{
    public interface IRAListingBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10);
        Task<ResponseDto> Put(Guid id, RAListingDto rAListingDto);
        Task<ResponseDto> GetInvoice(Guid id, int page = 1, int pageSize = 10);
    }
}
