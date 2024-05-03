using MediatR;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Profiles;
using AdminDashboardService.Commands;

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
        var adagencyToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.AdvertisingAgency);
        _dbContext.Update(adagencyToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return adagencyToUpdate;
    }
}