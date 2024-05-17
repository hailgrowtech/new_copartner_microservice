using MediatR;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Profiles;
using AdminDashboardService.Commands;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardService.Handlers;
public class PatchAdAgencyDetailsHandler : IRequestHandler<PatchAdAgencyDetailsCommand, AdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchAdAgencyDetailsHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<AdvertisingAgency> Handle(PatchAdAgencyDetailsCommand command, CancellationToken cancellationToken)
    {
        // Fetch the current entity from the database without tracking it
        var currentAdAgency = await _dbContext.AdvertisingAgencies.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentAdAgency == null)
        {
            // Handle the case where the expert does not exist
            throw new Exception($"AdAgency with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var AdAgencyToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentAdAgency);
        AdAgencyToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.AdvertisingAgencies.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(AdAgencyToUpdate);
        _dbContext.Entry(AdAgencyToUpdate).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return AdAgencyToUpdate;
    }
}