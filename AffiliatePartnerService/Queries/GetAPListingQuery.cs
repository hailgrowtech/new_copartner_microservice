using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AffiliatePartnerService.Queries;
public record GetAPListingQuery : IRequest<IEnumerable<APListingDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetAPListingQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}

