using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetUserDataListingQuery : IRequest<IEnumerable<UserDataListingDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetUserDataListingQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}