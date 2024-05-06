using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetRelationshipManagerByIdQuery(Guid Id) : IRequest<RelationshipManager>;

