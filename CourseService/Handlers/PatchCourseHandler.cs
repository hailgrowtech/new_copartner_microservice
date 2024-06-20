using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using ExpertService.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class PatchCourseHandler : IRequestHandler<PatchCourseCommand, Course>
    {
        private readonly CourseDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchCourseHandler(CourseDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<Course> Handle(PatchCourseCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentCourse = await _dbContext.Courses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentCourse == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Course  with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var courseToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentCourse);
            courseToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.Courses.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(courseToUpdate);
            _dbContext.Entry(courseToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return courseToUpdate;

        }
    }
}
