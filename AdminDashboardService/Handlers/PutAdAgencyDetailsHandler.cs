using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Handlers
{
    public class PutAdAgencyDetailsHandler : IRequestHandler<PutAdAgencyDetailsCommand, AdvertisingAgency>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutAdAgencyDetailsHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdvertisingAgency> Handle(PutAdAgencyDetailsCommand request, CancellationToken cancellationToken)
        {
            var entity = request.AdvertisingAgency;

            var existingEntity = await _dbContext.AdvertisingAgencies.FindAsync(entity.Id);
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
