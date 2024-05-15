using AuthenticationService.Models;
using MassTransit.Courier.Contracts;
using MediatR;

namespace AuthenticationService.Commands
{

    public record PutUserCommand(AuthenticationDetail Users) : IRequest<AuthenticationDetail>;

}
