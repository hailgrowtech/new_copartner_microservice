using FluentValidation;

using AdvertisingAgencyService.Commands;

namespace AdvertisingAgencyService.Validators;
public class CreateAdAgencyDetailsCommandValidator : AbstractValidator<CreateAdAgencyDetailsCommand>
{
    public CreateAdAgencyDetailsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.AdvertisingAgency.AgencyName).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}