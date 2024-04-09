using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;

namespace CourseService.Handlers;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Course>
{
    private readonly CourseDbContext _dbContext;
    public CreateCourseHandler(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Course;
        await _dbContext.Courses.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Course.Id = entity.Id;
        return request.Course;
    }
}
