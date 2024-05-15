using FluentValidation;

using AdminDashboardService.Commands;

namespace AdminDashboardService.Validators;
public class CreateAdAgencyDetailsCommandValidator : AbstractValidator<CreateAdAgencyDetailsCommand>
{
    public CreateAdAgencyDetailsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.AdvertisingAgency.AgencyName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.AdvertisingAgency.Link).NotEmpty().MaximumLength(500);
    }

}