using AffiliatePartnerService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace ExpertsService.Handlers
{
    public class PutAffiliatePartnerHandler : IRequestHandler<PutAffiliatePartnerCommand, AffiliatePartner>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutAffiliatePartnerHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AffiliatePartner> Handle(PutAffiliatePartnerCommand request, CancellationToken cancellationToken)
        {
            var entity = request.AffiliatePartner;

            var existingEntity = await _dbContext.AffiliatePartners.FindAsync(entity.Id);
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
