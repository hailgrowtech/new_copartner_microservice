using MediatR;
using AffiliatePartnerService.Commands;

using AffiliatePartnerService.Profiles;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using Microsoft.EntityFrameworkCore;

namespace AffiliatePartnerService.Handlers;
public class PatchAffiliatePartnerHandler : IRequestHandler<PatchAffiliatePartnerCommand, AffiliatePartner>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchAffiliatePartnerHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<AffiliatePartner> Handle(PatchAffiliatePartnerCommand command, CancellationToken cancellationToken)
    {
        // Fetch the current entity from the database without tracking it
        var currentAP= await _dbContext.AffiliatePartners.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentAP == null)
        {
            // Handle the case where the expert does not exist
            throw new Exception($"Expert with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var apToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentAP);
        apToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.AffiliatePartners.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(apToUpdate);
        _dbContext.Entry(apToUpdate).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return apToUpdate;
    }
}