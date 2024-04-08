using FluentValidation;
using ExpertService.Commands;

namespace ExpertService.Validators;
public class CreateExpertsCommandValidator : AbstractValidator<CreateExpertsCommand>
{
    public CreateExpertsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.Experts.Name).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}