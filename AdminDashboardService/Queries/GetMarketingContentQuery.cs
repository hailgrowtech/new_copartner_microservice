using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetMarketingContentQuery : IRequest<IEnumerable<MarketingContent>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetMarketingContentQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


