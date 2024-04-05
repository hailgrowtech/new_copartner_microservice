using MediatR;
using ExpertService.Models;

namespace ExpertService.Queries;
public record GetExpertsQuery : IRequest<IEnumerable<Experts>>;


 
