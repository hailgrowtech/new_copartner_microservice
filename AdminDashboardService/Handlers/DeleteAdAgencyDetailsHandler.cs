using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Handlers;
public class DeleteAdAgencyDetailsHandler : IRequestHandler<DeleteAdAgencyDetailsCommand, AdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteAdAgencyDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<AdvertisingAgency> Handle(DeleteAdAgencyDetailsCommand request, CancellationToken cancellationToken)
    {
        var adagency = await _dbContext.AdvertisingAgencies.FindAsync(request.Id);
        if (adagency == null) return null; // or throw an exception indicating the entity not found

        adagency.IsDeleted = true; // Mark the entity as deleted
        adagency.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        adagency.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return adagency; // Return the deleted entity
    }
}
