using CommonLibrary.CommonDTOs;
using ExpertService.Dtos;
using ExpertsService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ExpertsService.Logic
{
    public interface IExpertAvailabilityBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(ExpertAvailabilityCreateDto expertAvailability);
        Task<ResponseDto> Put(Guid id, ExpertAvailabilityCreateDto expertAvailabilityCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertAvailabilityCreateDto> expertavailabilityDto);
        Task<ResponseDto> Delete(Guid id);
    }
}
