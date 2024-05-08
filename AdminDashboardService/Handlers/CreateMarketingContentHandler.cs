using AdminDashboardService.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{

    public class CreateMarketingContentHandler : IRequestHandler<CreateMarketingServiceCommand, MarketingContent>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateMarketingContentHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MarketingContent> Handle(CreateMarketingServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = request.MarketingContent;
            // Check if the title is unique
            bool isUnique = await IsBlogBannerNameUnique(entity.BannerName);
            if (!isUnique)
            {
                return null;
            }
            await _dbContext.MarketingContents.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.MarketingContent.Id = entity.Id;
            return request.MarketingContent;
        }

        private async Task<bool> IsBlogBannerNameUnique(string bannerName)
        {
            // Normalize input to lowercase
            string lowerCaseBannerName = bannerName.ToLower();

            // Check if any existing entity has the same Name (case-insensitive)
            var existingEntity = await _dbContext.MarketingContents
                .FirstOrDefaultAsync(a => a.BannerName.ToLower() == lowerCaseBannerName);

            // Return true if no existing entity is found, indicating uniqueness
            return existingEntity == null;
        }
    }
}
