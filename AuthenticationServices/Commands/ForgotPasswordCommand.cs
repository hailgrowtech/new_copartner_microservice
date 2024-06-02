using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Commands
{
    public record ForgotPasswordCommand(ForgotPassword ForgotPassword) : IRequest<bool>;
}
