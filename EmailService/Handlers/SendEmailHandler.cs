using System.Threading;
using System.Threading.Tasks;
using EmailService.Commands;
using EmailService.Logic;
using MediatR;

namespace EmailService.Handlers
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IEmailBusinessProcessor _emailService;

        public SendEmailHandler(IEmailBusinessProcessor emailService)
        {
            _emailService = emailService;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return true;
        }
    }
}
