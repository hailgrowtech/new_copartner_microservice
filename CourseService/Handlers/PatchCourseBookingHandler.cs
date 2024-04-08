using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using ExpertService.Profiles;
using MediatR;

namespace CourseService.Handlers
{
    public class PatchCourseBookingHandler : IRequestHandler<PatchCourseBookingCommand, CourseBooking>
    {
        private readonly CourseDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchCourseBookingHandler(CourseDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<CourseBooking> Handle(PatchCourseBookingCommand command, CancellationToken cancellationToken)
        {
            var courseToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.course);
            _dbContext.Update(courseToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return courseToUpdate;
        }
    }
}
