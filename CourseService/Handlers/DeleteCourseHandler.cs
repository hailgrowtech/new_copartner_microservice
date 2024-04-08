using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Course>
    {
        private readonly CourseDbContext _dbContext;
        public DeleteCourseHandler(CourseDbContext dbContext) => _dbContext = dbContext;
        public async Task<Course> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseList = await _dbContext.Courses.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (courseList == null) return null;
            _dbContext.Courses.Remove(courseList);
            await _dbContext.SaveChangesAsync();
            return courseList;
        }
    }
}
