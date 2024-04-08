using CommonLibrary.CommonDTOs;
using CourseService.Dtos;
using Microsoft.AspNetCore.JsonPatch;
namespace CourseService.Logic
{
    public interface ICourseBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(CourseCreateDto experts);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseCreateDto> expertsDto);
        //bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO);
    }
}
