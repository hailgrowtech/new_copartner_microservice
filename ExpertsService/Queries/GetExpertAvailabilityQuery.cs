using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries;


public record GetExpertAvailabilityQuery : IRequest<IEnumerable<ExpertAvailability>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetExpertAvailabilityQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}