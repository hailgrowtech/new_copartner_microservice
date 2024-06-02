using AuthenticationService.Dtos;
using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Commands;

public record ResetForgotPasswordCommand(ResetPasswordDTO ResetPassword) : IRequest<bool>;
