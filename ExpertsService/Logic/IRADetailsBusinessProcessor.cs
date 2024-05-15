using CommonLibrary.CommonDTOs;
using ExpertsService.Dtos;

namespace ExpertsService.Logic
{
    public interface IRADetailsBusinessProcessor
    {
        Task<ResponseDto> Get(bool isCoPartner,int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10);
        Task<ResponseDto> Put(Guid id, RADetailsDto rAListingDetailsDto);
    }
}
