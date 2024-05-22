using MediatR;
using MigrationDB.Models;


namespace ExpertService.Queries;

public record GetExpertsQuery : IRequest<IEnumerable<Experts>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetExpertsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}




