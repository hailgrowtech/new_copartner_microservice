using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Queries;
public record GetUserByEmailQuery(AuthenticationDetail Users) : IRequest<AuthenticationDetail>;



