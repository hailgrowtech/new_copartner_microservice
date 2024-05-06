using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;


public record GetRelationshipManagerQuery : IRequest<IEnumerable<RelationshipManager>>;
