using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PutRelationshipManagerCommand(RelationshipManager RelationshipManager) : IRequest<RelationshipManager>;
