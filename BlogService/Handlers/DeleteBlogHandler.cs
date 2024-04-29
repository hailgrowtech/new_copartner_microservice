using BlogService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace BlogService.Handlers;
public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommand, Blog>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteBlogHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<Blog> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var experts = await _dbContext.Blogs.FindAsync(request.Id);
        if (experts == null) return null; // or throw an exception indicating the entity not found

        experts.IsDeleted = true; // Mark the entity as deleted
        experts.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        experts.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return experts; // Return the deleted entity
    }
}
