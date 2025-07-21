namespace Jobber.Auth.Application.Contracts;

public interface IRefreshTokenGenerator
{
    string GenerateToken();
}