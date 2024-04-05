using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Queries;
public record GetUserAuthDetailsQuery(AuthenticationDetail AuthenticationDetail) : IRequest<AuthenticationDetail>;

