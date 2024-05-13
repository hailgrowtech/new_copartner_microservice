using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;


public record GetJoinByIdQuery(Guid Id) : IRequest<Join>;
