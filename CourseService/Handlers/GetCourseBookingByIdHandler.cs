using CourseService.Data;
using CourseService.Models;
using CourseService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class GetCourseBookingByIdHandler : IRequestHandler<GetCourseBookingByIdQuery, CourseBooking>
    {
        private readonly CourseDbContext _dbContext;
        public GetCourseBookingByIdHandler(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CourseBooking> Handle(GetCourseBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var courseBookingList = await _dbContext.CourseBookings.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return courseBookingList;
        }
    }
}
