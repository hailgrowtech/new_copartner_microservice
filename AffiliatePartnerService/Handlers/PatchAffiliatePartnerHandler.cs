using MediatR;
using AffiliatePartnerService.Commands;

using AffiliatePartnerService.Profiles;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;

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
        var affiliatePartnersToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.AffiliatePartner);
        _dbContext.Update(affiliatePartnersToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return affiliatePartnersToUpdate;
    }
}