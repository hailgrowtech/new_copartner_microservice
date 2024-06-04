using AuthenticationService.Models;
using MediatR;
using System.Net;

namespace AuthenticationService.Commands;
public record ForgotPasswordCommand(ForgotPassword ForgotPassword) : IRequest<HttpStatusCode>;
