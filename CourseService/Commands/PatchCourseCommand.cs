using CourseService.Dtos;
using CourseService.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace CourseService.Commands
{
    public record PatchCourseCommand(Guid Id, JsonPatchDocument<CourseCreateDto> JsonPatchDocument, Course course) : IRequest<Course>;
}
