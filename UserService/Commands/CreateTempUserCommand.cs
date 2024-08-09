using MediatR;
using MigrationDB.Models;


namespace UserService.Commands;

public record CreateTempUserCommand(TempUser TempUser) : IRequest<TempUser>;

