using MassTransit.Courier.Contracts;
using MediatR;
using MigrationDB.Models;

namespace UserService.Commands
{

    public record PutUserCommand(User user) : IRequest<User>;

}
