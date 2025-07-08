namespace Jobber.Auth.Application.Interfaces;

public interface IEmailConfirmationService
{
    Task<string> GenerateEmailConfirmationToken(string userId);
        
}