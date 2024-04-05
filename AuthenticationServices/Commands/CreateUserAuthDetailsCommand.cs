using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Commands;

    public record CreateUserAuthDetailsCommand(AuthenticationDetail User) : IRequest<AuthenticationDetail>;

