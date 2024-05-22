using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetUserSecondTimePaymentListingQuery : IRequest<IEnumerable<UserSecondTimePaymentListingDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetUserSecondTimePaymentListingQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}

