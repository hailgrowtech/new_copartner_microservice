using CourseService.Models;
using MediatR;

namespace CourseService.Queries
{
    public record GetCourseByIdQuery(Guid Id) : IRequest<Course>;
}
