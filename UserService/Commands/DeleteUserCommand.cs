using MediatR;
using UserService.Models;
namespace UserService.Commands
{
    public record DeleteUserCommand (Guid Id) : IRequest<User>;

}
