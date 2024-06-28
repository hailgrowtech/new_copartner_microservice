using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers;
public class GetMarketingContentHandler : IRequestHandler<GetMarketingContentQuery, IEnumerable<MarketingContent>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetMarketingContentHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<MarketingContent>> Handle(GetMarketingContentQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;


        var entities =  await _dbContext.MarketingContents.Where(x => x.IsDeleted != true)
                                .OrderByDescending(x => x.CreatedOn)
                                .Skip(skip)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken);
        if (entities == null) return null; 
        return entities;

    }
}
