using ContactUsService.Dto;
using FluentValidation;
namespace ContactUsService.Validators;
public class CreateExpertsCommandValidator : AbstractValidator<SendEmailCommand>
{
    public CreateExpertsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}