using MediatR;
using MigrationDB.Models;


namespace ExpertService.Queries;
public record GetExpertsListingQuery : IRequest<IEnumerable<Experts>>;


