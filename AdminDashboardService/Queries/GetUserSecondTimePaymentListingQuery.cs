using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetUserSecondTimePaymentListingQuery : IRequest<IEnumerable<UserSecondTimePaymentListingDto>>;

