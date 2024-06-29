using MediatR;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;
using AdminDashboardService.Profiles;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.CommonModels;

namespace AdminDashboardService.Handlers;
public class PatchBlogHandler : IRequestHandler<PatchBlogCommand, Blog>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchBlogHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<Blog> Handle(PatchBlogCommand command, CancellationToken cancellationToken)
    {

        // Fetch the current entity from the database without tracking it
        var currentBlogs = await _dbContext.Blogs.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentBlogs == null)
        {
            // Handle the case where the subscriber does not exist
            throw new Exception($"Blogs with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var BlogsToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentBlogs);
        BlogsToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.Blogs.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(BlogsToUpdate);
        _dbContext.Entry(BlogsToUpdate).State = EntityState.Modified;
        // Preserve multiple properties 
        _dbContext.PreserveProperties(trackedEntity, currentBlogs, "CreatedOn");

        await _dbContext.SaveChangesAsync(cancellationToken);

        return BlogsToUpdate;

    }
}