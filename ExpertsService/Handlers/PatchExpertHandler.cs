using MediatR;
using ExpertService.Commands;
using ExpertService.Profiles;
using MigrationDB.Models;
using MigrationDB.Data;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.CommonModels;

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
        // Fetch the current entity from the database without tracking it
        var currentExpert = await _dbContext.Experts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentExpert == null)
        {
            // Handle the case where the expert does not exist
            throw new Exception($"Expert with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var expertsToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentExpert);
        expertsToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.Experts.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(expertsToUpdate);
        _dbContext.Entry(expertsToUpdate).State = EntityState.Modified;

        // Preserve multiple properties 
        _dbContext.PreserveProperties(expertsToUpdate, currentExpert, "CreatedOn");

        await _dbContext.SaveChangesAsync(cancellationToken);

        return expertsToUpdate;
    }


}