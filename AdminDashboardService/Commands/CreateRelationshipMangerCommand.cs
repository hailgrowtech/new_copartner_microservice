using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record CreateRelationshipMangerCommand(RelationshipManager RelationshipManager) : IRequest<RelationshipManager>;

