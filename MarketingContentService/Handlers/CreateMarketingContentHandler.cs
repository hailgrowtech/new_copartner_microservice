using MarketingContentService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace MarketingContentService.Handlers
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
            await _dbContext.MarketingContents.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.MarketingContent.Id = entity.Id;
            return request.MarketingContent;
        }
    }
}
