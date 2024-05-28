using MediatR;

namespace EmailService.Commands
{
    public record SendEmailCommand(string To, string Subject, string Body) : IRequest<bool>;
}
