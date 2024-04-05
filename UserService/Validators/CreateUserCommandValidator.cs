using FluentValidation;
using UserService.Commands;

namespace UserService.Validators;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.User.Name).NotEmpty().MaximumLength(300);
    }
}