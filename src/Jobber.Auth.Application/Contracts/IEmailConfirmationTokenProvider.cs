namespace Jobber.Auth.Application.Contracts;

public interface IEmailConfirmationTokenProvider
{
    string GenerateEmailConfirmationToken();
}