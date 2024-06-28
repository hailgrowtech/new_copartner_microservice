using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetMarketingContentByContentTypeHandler : IRequestHandler<GetMarketingContentByContentTypeQuery, IEnumerable<MarketingContent>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetMarketingContentByContentTypeHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<MarketingContent>> Handle(GetMarketingContentByContentTypeQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;


            var entities = await _dbContext.MarketingContents.Where(x => x.IsDeleted != true && x.ContentType == request.ContentType)
                                    .OrderByDescending(x => x.CreatedOn)
                                    .Skip(skip)
                                    .Take(request.PageSize)
                                    .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;

        }
    }
}
