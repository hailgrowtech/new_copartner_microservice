using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using ExpertService.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            // Fetch the current entity from the database without tracking it
            var currentCourseBooking = await _dbContext.CourseBookings.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentCourseBooking == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Course Booking with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var courseBookingToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentCourseBooking);
            courseBookingToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.CourseBookings.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(courseBookingToUpdate);
            _dbContext.Entry(courseBookingToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return courseBookingToUpdate;
        }
    }
}
