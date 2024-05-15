using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class PutJoinHandler : IRequestHandler<PutJoinCommand, Join>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutJoinHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Join> Handle(PutJoinCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Join;

            var existingEntity = await _dbContext.Joins.FindAsync(entity.Id);
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
