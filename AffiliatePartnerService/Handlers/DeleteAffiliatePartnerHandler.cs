using AffiliatePartnerService.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Handlers;
public class DeleteAffiliatePartnerHandler : IRequestHandler<DeleteAffiliatePartnerCommand, AffiliatePartner>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteAffiliatePartnerHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<AffiliatePartner> Handle(DeleteAffiliatePartnerCommand request, CancellationToken cancellationToken)
    {
        var affiliatePartners = await _dbContext.AffiliatePartners.FindAsync(request.Id);
        if (affiliatePartners == null) return null; // or throw an exception indicating the entity not found

        affiliatePartners.IsDeleted = true; // Mark the entity as deleted
        affiliatePartners.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        affiliatePartners.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return affiliatePartners; // Return the deleted entity
    }
}
