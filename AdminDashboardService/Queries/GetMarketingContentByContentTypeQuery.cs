using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetMarketingContentByContentTypeQuery : IRequest<IEnumerable<MarketingContent>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string ContentType { get; init; }

    public GetMarketingContentByContentTypeQuery(int page, int pageSize, string contentType)
    {
        Page = page;
        PageSize = pageSize;
        ContentType = contentType;
    }
}
