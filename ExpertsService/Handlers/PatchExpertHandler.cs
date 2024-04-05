using MediatR;
using ExpertService.Commands;
using ExpertService.Data;
using ExpertService.Models;
using ExpertService.Profiles;

namespace ExpertService.Handlers;
public class PatchExpertHandler : IRequestHandler<PatchExpertsCommand, Experts>
{
    private readonly ExpertsDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;
    public PatchExpertHandler(ExpertsDbContext dbContext,
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