using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;


public record GetUserFirstTimePaymentListingQuery : IRequest<IEnumerable<UserFirstTimePaymentListingDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetUserFirstTimePaymentListingQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
