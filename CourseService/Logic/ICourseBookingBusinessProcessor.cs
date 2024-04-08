using CommonLibrary.CommonDTOs;
using CourseService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace CourseService.Logic
{
    public interface ICourseBookingBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(CourseBookingCreateDto courseBooking);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseBookingCreateDto> courseBookingDto);
    }
}
