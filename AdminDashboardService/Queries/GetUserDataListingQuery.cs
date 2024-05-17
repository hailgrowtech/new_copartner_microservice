using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetUserDataListingQuery : IRequest<IEnumerable<UserDataListingDto>>;