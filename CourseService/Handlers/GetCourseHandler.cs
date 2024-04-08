using CourseService.Data;
using CourseService.Models;
using CourseService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers
{
    public class GetCourseHandler : IRequestHandler<GetCourseQuery, IEnumerable<Course>>
    {
        private readonly CourseDbContext _dbContext;
        public GetCourseHandler(CourseDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<Course>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Courses.ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }

    }
}
