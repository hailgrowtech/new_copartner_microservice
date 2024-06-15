using MediatR;
using SignInService.Models;

namespace SignInService.Queries;

public record GetUserDetailsByIdQuery(Guid Guid) : IRequest<PotentialCustomer>;



