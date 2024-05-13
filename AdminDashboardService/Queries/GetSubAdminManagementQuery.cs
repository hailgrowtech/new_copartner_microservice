using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetSubAdminManagementQuery : IRequest<IEnumerable<SubAdminManagementDto>>;

