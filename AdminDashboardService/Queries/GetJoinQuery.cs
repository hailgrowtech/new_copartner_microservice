using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;


public record GetJoinQuery : IRequest<IEnumerable<JoinReadDto>>;

