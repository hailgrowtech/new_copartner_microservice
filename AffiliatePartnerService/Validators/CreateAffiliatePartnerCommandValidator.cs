using AffiliatePartnerService.Commands;
using FluentValidation;

namespace AffiliatePartnerService.Validators;
public class CreateAffiliatePartnerCommandValidator : AbstractValidator<CreateAffiliatePartnerCommand>
{
    public CreateAffiliatePartnerCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.AffiliatePartner.Name).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}