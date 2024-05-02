using MarketingContentService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace MarketingContentService.Handlers
{
    public class PutMarketingContentHandler : IRequestHandler<PutMarketingServiceCommand, MarketingContent>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutMarketingContentHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MarketingContent> Handle(PutMarketingServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = request.MarketingContent;

            var existingEntity = await _dbContext.MarketingContents.FindAsync(entity.Id);
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
