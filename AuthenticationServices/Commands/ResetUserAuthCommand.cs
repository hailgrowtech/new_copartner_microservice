using AuthenticationService.Dtos;
using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Commands;

public record ResetUserAuthCommand(UserPasswordDTO User) : IRequest<bool>;
