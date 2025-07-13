using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Application.Options;
using Jobber.Auth.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Jobber.Auth.Application.Auth.Services;

public class RefreshTokenFactory(
    IRefreshTokenProvider refreshTokenProvider, 
    IOptions<RefreshTokenOptions> options) 
    : IRefreshTokenFactory
{
    private readonly IRefreshTokenProvider _refreshTokenProvider = refreshTokenProvider;
    private readonly RefreshTokenOptions _options = options.Value;

    public RefreshToken Create(Guid userId)
    {
        var newToken = new RefreshToken
        {
            UserId = userId,
            Token = _refreshTokenProvider.GenerateToken(),
            IssuedAt = DateTimeOffset.UtcNow,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(_options.ExpiresDays),
            IsRevoked = false
        };
        return newToken;
    }
}