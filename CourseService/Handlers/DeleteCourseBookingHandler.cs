using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class DeleteCourseBookingHandler : IRequestHandler<DeleteCourseBookingCommand, CourseBooking>
    {
        private readonly CourseDbContext _dbContext;
        public DeleteCourseBookingHandler(DeleteCourseBookingCommand dbContext) => _dbContext = dbContext;
        public async Task<CourseBooking> Handle(DeleteCourseBookingCommand request, CancellationToken cancellationToken)
        {
            var courseBookingList = await _dbContext.CourseBookings.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (courseBookingList == null) return null;
            _dbContext.CourseBookings.Remove(courseBookingList);
            await _dbContext.SaveChangesAsync();
            return courseBookingList;
        }
    }
}
