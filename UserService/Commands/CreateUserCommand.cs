using MediatR;
using MigrationDB.Models;


namespace UserService.Commands;

public record CreateUserCommand(User User) : IRequest<User>;

