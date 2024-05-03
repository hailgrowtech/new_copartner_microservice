using MediatR;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;
using AdminDashboardService.Profiles;

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
        var expertsToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.Blog);
        _dbContext.Update(expertsToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return expertsToUpdate;
    }
}