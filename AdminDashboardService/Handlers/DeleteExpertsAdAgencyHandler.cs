using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers;
public class DeleteExpertsAdAgencyHandler : IRequestHandler<DeleteExpertsAdAgencyCommand, ExpertsAdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteExpertsAdAgencyHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<ExpertsAdvertisingAgency> Handle(DeleteExpertsAdAgencyCommand request, CancellationToken cancellationToken)
    {
        var adagency = await _dbContext.ExpertsAdvertisingAgencies.FindAsync(request.Id);
        if (adagency == null) return null; // or throw an exception indicating the entity not found

        adagency.IsDeleted = true; // Mark the entity as deleted
        adagency.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        adagency.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return adagency; // Return the deleted entity
    }
}
