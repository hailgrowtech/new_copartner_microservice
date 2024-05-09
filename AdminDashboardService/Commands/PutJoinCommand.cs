using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PutJoinCommand(Join Join) : IRequest<Join>;
