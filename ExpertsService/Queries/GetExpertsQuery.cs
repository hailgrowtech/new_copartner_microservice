using MediatR;
using MigrationDB.Models;


namespace ExpertService.Queries;

public record GetExpertsQuery : IRequest<IEnumerable<Experts>>;




