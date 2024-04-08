using CourseService.Models;
using MediatR;

namespace CourseService.Commands
{
    public record DeleteCourseBookingCommand(Guid Id) : IRequest<CourseBooking>;
}
