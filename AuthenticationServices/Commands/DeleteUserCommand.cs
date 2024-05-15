using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Commands;
public record DeleteUserCommand(Guid Id) : IRequest<AuthenticationDetail>;
