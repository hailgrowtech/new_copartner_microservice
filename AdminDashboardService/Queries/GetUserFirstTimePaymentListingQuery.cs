using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;


public record GetUserFirstTimePaymentListingQuery : IRequest<IEnumerable<UserFirstTimePaymentListingDto>>;
