using MediatR;
using ExpertService.Commands;

using ExpertService.Profiles;
using MigrationDB.Models;
using MigrationDB.Data;

namespace ExpertService.Handlers;
public class PatchExpertHandler : IRequestHandler<PatchExpertsCommand, Experts>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchExpertHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<Experts> Handle(PatchExpertsCommand command, CancellationToken cancellationToken)
    {
        var expertsToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.Experts);
        _dbContext.Update(expertsToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return expertsToUpdate;
    }
}