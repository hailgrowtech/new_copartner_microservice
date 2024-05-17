using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;


public record GetRelationshipManagerQuery : IRequest<IEnumerable<RelationshipManager>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetRelationshipManagerQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
