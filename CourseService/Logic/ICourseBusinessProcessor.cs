using CommonLibrary.CommonDTOs;
using CourseService.Dtos;
using Microsoft.AspNetCore.JsonPatch;
namespace CourseService.Logic
{
    public interface ICourseBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(CourseCreateDto courses);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseCreateDto> courseDto);
        //bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO);
    }
}
