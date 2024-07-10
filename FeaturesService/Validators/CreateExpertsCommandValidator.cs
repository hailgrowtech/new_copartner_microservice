using FluentValidation;
using FeaturesService.Commands;

namespace FeaturesService.Validators;
public class CreateExpertsCommandValidator : AbstractValidator<CreateWebinarMstCommand>
{
    public CreateExpertsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.WebinarMst.Description).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}