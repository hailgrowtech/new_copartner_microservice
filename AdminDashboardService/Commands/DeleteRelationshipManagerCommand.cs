using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;

public record DeleteRelationshipManagerCommand(Guid Id) : IRequest<RelationshipManager>;

