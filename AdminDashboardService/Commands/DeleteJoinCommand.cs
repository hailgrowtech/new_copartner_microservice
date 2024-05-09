using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;

public record DeleteJoinCommand(Guid Id) : IRequest<Join>;
