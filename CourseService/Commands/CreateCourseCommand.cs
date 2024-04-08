using CourseService.Models;
using MediatR;

namespace CourseService.Commands;

    public record CreateCourseCommand(Course course) : IRequest<Course>;
