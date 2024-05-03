using FluentValidation;

using AdminDashboardService.Commands;

namespace AdminDashboardService.Validators;
public class CreateAdAgencyDetailsCommandValidator : AbstractValidator<CreateAdAgencyDetailsCommand>
{
    public CreateAdAgencyDetailsCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.AdvertisingAgency.AgencyName).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}