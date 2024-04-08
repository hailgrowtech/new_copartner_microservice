using CourseService.Models;
using MediatR;

namespace CourseService.Commands
{
    public record DeleteCourseCommand(Guid Id) : IRequest<Course>;
}
