using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetSubAdminManagementByIdQuery(Guid Id) : IRequest<SubAdminManagementReadDto>;

