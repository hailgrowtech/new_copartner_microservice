using CourseService.Models;
using MediatR;

namespace CourseService.Queries
{
    public record GetCourseBookingQuery : IRequest<IEnumerable<CourseBooking>>;
}
