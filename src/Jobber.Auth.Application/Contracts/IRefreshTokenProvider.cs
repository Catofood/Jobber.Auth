namespace Jobber.Auth.Application.Contracts;

public interface IRefreshTokenProvider
{
    string GenerateToken();
}