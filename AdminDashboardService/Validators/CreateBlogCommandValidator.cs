using FluentValidation;

using AdminDashboardService.Commands;

namespace AdminDashboardService.Validators;
public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        //RuleFor(x => x.User.Id).NotEmpty();
        RuleFor(x => x.Blog.Title).NotEmpty().MaximumLength(300);
       // RuleFor(x => x.User.LastName).NotEmpty().MaximumLength(100);
    }
}