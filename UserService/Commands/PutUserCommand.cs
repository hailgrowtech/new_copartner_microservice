using MassTransit.Courier.Contracts;
using MediatR;
using MigrationDB.Models;

namespace UserService.Commands
{

    public record PutUserCommand(string?MobileNo,User user) : IRequest<User>;

}
