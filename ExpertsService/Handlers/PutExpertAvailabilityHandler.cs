using ExpertService.Commands;
using ExpertsService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class PutExpertAvailabilityHandler : IRequestHandler<PutExpertAvailabilityCommand, ExpertAvailability>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutExpertAvailabilityHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExpertAvailability> Handle(PutExpertAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ExpertAvailability;

            var existingEntity = await _dbContext.ExpertAvailabilities.FindAsync(entity.Id);
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
