using CourseService.Models;
using MediatR;

namespace CourseService.Commands
{
    public record CreateCourseBookingCommand(CourseBooking courseBooking) : IRequest<CourseBooking>;

}
