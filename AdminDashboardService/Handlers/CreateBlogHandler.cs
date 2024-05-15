using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;
using Microsoft.EntityFrameworkCore;


namespace AdminDashboardService.Handlers;

public class  CreateBlogHandler : IRequestHandler<CreateBlogCommand, Blog>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateBlogHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Blog> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Blog;
        // Check if the title is unique
        bool isUnique = await IsBlogTitleUnique(entity.Title);
        if (!isUnique)
        {
            return null;
        }

        await _dbContext.Blogs.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Blog.Id = entity.Id;
        return request.Blog;
    }
    private async Task<bool> IsBlogTitleUnique(string title)
    {
        // Normalize input to lowercase
        string lowerCasetitle = title.ToLower();

        // Check if any existing entity has the same Title (case-insensitive)
        var existingEntity = await _dbContext.Blogs
            .FirstOrDefaultAsync(a => a.Title.ToLower() == lowerCasetitle);

        // Return true if no existing entity is found, indicating uniqueness
        return existingEntity == null;
    }
}