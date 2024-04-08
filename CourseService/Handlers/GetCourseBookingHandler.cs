using CourseService.Data;
using CourseService.Models;
using CourseService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class GetCourseBookingHandler : IRequestHandler<GetCourseBookingQuery, IEnumerable<CourseBooking>>
    {
        private readonly CourseDbContext _dbContext;
        public GetCourseBookingHandler(CourseDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<CourseBooking>> Handle(GetCourseBookingQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.CourseBookings.ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }

    }
}
