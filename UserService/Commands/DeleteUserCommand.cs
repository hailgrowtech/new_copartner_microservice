using MediatR;
using MigrationDB.Models;

namespace UserService.Commands;
public record DeleteUserCommand (Guid Id) : IRequest<User>;
