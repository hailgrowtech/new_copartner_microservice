using CommonLibrary.CommonDTOs;
using ExpertService.Dtos;
using ExpertsService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ExpertsService.Logic
{
    public interface IWebinarBookingBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(WebinarBookingCreateDto webinarBookingCreateDto);
        Task<ResponseDto> Put(Guid id, WebinarBookingCreateDto webinarBookingCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<WebinarBookingCreateDto> webinarBookingDto);
    }
}
