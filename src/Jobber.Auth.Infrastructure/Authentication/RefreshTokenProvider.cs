using System.Security.Cryptography;
using Jobber.Auth.Application.Contracts;

namespace Jobber.Auth.Infrastructure.Authentication;

public class RefreshTokenProvider : IRefreshTokenProvider
{
    public string GenerateToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}