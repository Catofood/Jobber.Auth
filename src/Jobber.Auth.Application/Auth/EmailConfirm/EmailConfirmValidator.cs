using FluentValidation;

namespace Jobber.Auth.Application.Auth.EmailConfirm;

public class EmailConfirmValidator : AbstractValidator<EmailConfirmCommand>
{
    public EmailConfirmValidator()
    {
        RuleFor(x => x.EmailConfirmDto).NotNull();
        RuleFor(x => x.EmailConfirmDto.Token).NotNull().NotEmpty();
        RuleFor(x => x.EmailConfirmDto.UserId).NotNull().NotEmpty();
    }
}