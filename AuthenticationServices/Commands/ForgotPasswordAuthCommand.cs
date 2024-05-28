using AuthenticationService.Dtos;
using MediatR;

namespace AuthenticationService.Commands
{
    public record ForgotPasswordCommand(UserPasswordDTO User) : IRequest<bool>;
}
