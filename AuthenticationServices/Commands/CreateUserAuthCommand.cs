using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Commands;

public record CreateUserAuthCommand(Authentication User) : IRequest<Authentication>;
