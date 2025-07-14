using FluentValidation;

namespace Jobber.Auth.Application.Auth.UpdateAuthTokens;

public class UpdateAuthTokensCommandValidator : AbstractValidator<UpdateAuthTokensCommand>
{
    public UpdateAuthTokensCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotNull().NotEmpty();
    }
}