using CourseService.Models;
using MediatR;

namespace CourseService.Commands
{
    public class DeleteCourseCommand(Guid Id) : IRequest<Course>;
}
