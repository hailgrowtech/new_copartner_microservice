using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;

namespace CourseService.Handlers
{
    public class CreateCourseBookingHandler : IRequestHandler<CreateCourseBookingCommand, CourseBooking>
    {
        private readonly CourseDbContext _dbContext;
        public CreateCourseBookingHandler(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CourseBooking> Handle(CreateCourseBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = request.courseBooking;
            await _dbContext.CourseBookings.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.courseBooking.Id = entity.Id;
            return request.courseBooking;
        }
    }
}
