using MediatR;
using MigrationDB.Models;


namespace ExpertService.Queries;
public record GetExpertsByIdQuery(Guid Id) : IRequest<Experts>;


 
