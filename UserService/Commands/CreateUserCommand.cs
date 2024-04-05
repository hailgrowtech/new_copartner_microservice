using MediatR;
using UserService.Models;

namespace UserService.Commands;

public record CreateUserCommand(User User) : IRequest<User>;

