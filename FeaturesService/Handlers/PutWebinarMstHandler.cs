using FeaturesService.Commands;
using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class PutWebinarMstHandler : IRequestHandler<PutWebinarMstCommand, WebinarMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutWebinarMstHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebinarMst> Handle(PutWebinarMstCommand request, CancellationToken cancellationToken)
        {
            var entity = request.webinarMst;

            var existingEntity = await _dbContext.WebinarMsts.FindAsync(entity.Id);
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
