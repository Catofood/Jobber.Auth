namespace Jobber.Auth.Application.Contracts;

public interface IEmailConfirmationService
{
    Task<string> GenerateEmailConfirmationToken(string userId);
}