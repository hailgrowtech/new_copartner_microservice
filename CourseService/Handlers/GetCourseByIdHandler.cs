using CourseService.Data;
using CourseService.Models;
using CourseService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Course>
    {
        private readonly CourseDbContext _dbContext;
        public GetCourseByIdHandler(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var courseList = await _dbContext.Courses.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return courseList;
        }
    }
}
