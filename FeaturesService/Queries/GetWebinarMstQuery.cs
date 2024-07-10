using FeaturesService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;

public record GetWebinarMstQuery : IRequest<IEnumerable<WebinarMst>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetWebinarMstQuery( int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}