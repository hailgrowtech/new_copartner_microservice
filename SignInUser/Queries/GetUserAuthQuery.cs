using MediatR;
using SignInService.Models;

namespace SignInService.Queries;
public record GetUserAuthQuery(PotentialCustomer Lead, string Token) : IRequest<PotentialCustomer>;