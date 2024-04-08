using CourseService.Dtos;
using CourseService.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace CourseService.Commands
{
    public record PatchCourseBookingCommand(Guid Id, JsonPatchDocument<CourseBookingCreateDto> JsonPatchDocument, CourseBooking course) : IRequest<CourseBooking>;
}
