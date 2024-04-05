using MediatR;
using ExpertService.Models;

namespace ExpertService.Queries;
public record GetExpertsByMobileNumberOrEmailQuery(Experts Experts) : IRequest<Experts>;



