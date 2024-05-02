using MarketingContentService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace MarketingContentService.Handlers;
public class GetMarketingContentHandler : IRequestHandler<GetMarketingContentQuery, IEnumerable<MarketingContent>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetMarketingContentHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<MarketingContent>> Handle(GetMarketingContentQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.MarketingContents.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}
