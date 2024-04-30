using CommonLibrary.CommonDTOs;
using ExpertsService.Dtos;

namespace ExpertsService.Logic
{
    public interface IRAListingDetailsBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Put(Guid id, RAListingDetailsDto rAListingDetailsDto);
    }
}
