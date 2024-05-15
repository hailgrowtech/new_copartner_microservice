using AuthenticationService.Models;
using MediatR;



namespace AuthenticationService.Commands;

public record CreateUserCommand(AuthenticationDetail Users) : IRequest<AuthenticationDetail>;

