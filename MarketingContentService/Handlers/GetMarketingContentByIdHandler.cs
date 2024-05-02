using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using MigrationDB.Data;
using MarketingContentService.Queries;
using MigrationDB.Model;

namespace MarketingContentService.Handlers;

public class GetMarketingContentByIdHandler : IRequestHandler<GetMarketingContentByIdQuery, MarketingContent>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetMarketingContentByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MarketingContent> Handle(GetMarketingContentByIdQuery request, CancellationToken cancellationToken)
    {
        var marketingContentList = await _dbContext.MarketingContents.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return marketingContentList;
    }
}