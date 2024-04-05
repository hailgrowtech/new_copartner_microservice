using MediatR;
using ExpertService.Models;

namespace ExpertService.Queries;
public record GetExpertsByIdQuery(Guid Id) : IRequest<Experts>;


 
