using ExpertService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace ExpertsService.Handlers
{
    public class PutExpertsHandler : IRequestHandler<PutExpertsCommand, Experts>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutExpertsHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Experts> Handle(PutExpertsCommand request, CancellationToken cancellationToken)
        {
            var entity = request.experts;

            var existingEntity = await _dbContext.Experts.FindAsync(entity.Id);
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
