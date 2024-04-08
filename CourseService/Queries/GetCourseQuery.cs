using CourseService.Models;
using MediatR;

namespace CourseService.Queries
{
    public record GetCourseQuery : IRequest<IEnumerable<Course>>;

}
