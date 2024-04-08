using CourseService.Models;
using MediatR;

namespace CourseService.Commands
{
    public class CreateCourseCommand
    {
        public record CreateExpertsCommand(Course course) : IRequest<Course>;
    }
}
