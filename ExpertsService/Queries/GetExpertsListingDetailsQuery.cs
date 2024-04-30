using MediatR;
using MigrationDB.Models;


namespace ExpertService.Queries;
public record GetExpertsListingDetailsQuery : IRequest<IEnumerable<Experts>>;


