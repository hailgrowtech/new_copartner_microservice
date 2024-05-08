using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Handlers
{
    public class PutExpertsAdAgencyHandler : IRequestHandler<PutExpertsAdAgencyCommand, ExpertsAdvertisingAgency>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutExpertsAdAgencyHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExpertsAdvertisingAgency> Handle(PutExpertsAdAgencyCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ExpertsAdvertisingAgency;

            var existingEntity = await _dbContext.ExpertsAdvertisingAgencies.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }
    }
}
