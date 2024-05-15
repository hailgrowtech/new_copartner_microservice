using MediatR;
using ContactService.Commands;
using ContactUsService.Dto;
using ContactService.Dto;

namespace ContactService.Handlers;

public class SendContactUsFormHandler : IRequestHandler<SendContactUsFormCommand, bool>

{
    private readonly EmailService _emailService;

    public SendContactUsFormHandler(EmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<bool> Handle(SendContactUsFormCommand request, CancellationToken cancellationToken)
    {




        var command = new SendEmailCommand
        {
            ToEmail = "destination-email@example.com", // This can be dynamic based on the formId
            Subject = request.form.Subject,
            Body = $"Name: {request.form.Name}\nEmail: {request.form.Email}\nMessage: {request.form.Message}"
        };

        await _emailService.SendEmail(command);
        return true; // Return Unit to indicate successful handling
    }
}
  