using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetRelationshipManagerByUserIdQuery(Guid Id, string UserType) : IRequest<RelationshipManager>;

