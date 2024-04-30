using CommonLibrary.CommonDTOs;
using ExpertsService.Dtos;

namespace ExpertsService.Logic
{
    public interface IRAListingBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Put(Guid id, RAListingDto rAListingDto);
    }
}
