using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record CreateJoinCommand(Join Join) : IRequest<Join>;
