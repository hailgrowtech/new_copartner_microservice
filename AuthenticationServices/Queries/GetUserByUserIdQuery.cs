using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Queries;
public record GetUserByUserIdQuery(Guid Guid) : IRequest<Authentication>;