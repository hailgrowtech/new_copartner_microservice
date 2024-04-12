using FluentValidation;
using SubscriptionService.Commands;

namespace SubscriptionService.Validators;
public class CreateExpertsCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateExpertsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.Subscription.ServiceType).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}