using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using ExpertService.Profiles;
using MediatR;

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
            var courseToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.course);
            _dbContext.Update(courseToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return courseToUpdate;
        }
    }
}
