using CourseService.Models;
using MediatR;

namespace CourseService.Queries
{
    public record GetCourseBookingByIdQuery(Guid Id) : IRequest<CourseBooking>;
}
