using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Application.Options;
using Jobber.Auth.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Jobber.Auth.Infrastructure.Repositories;

public class RefreshTokenRepositoryDecorator(
    RefreshTokenRepository inner, 
    IOptions<RefreshTokenOptions> options) 
    : IRefreshTokenRepository
{
    private readonly RefreshTokenRepository _inner = inner;
    private readonly int _maxActiveRefreshTokens = options.Value.MaxActiveRefreshTokensPerUser;

    public async Task Add(RefreshToken token, CancellationToken cancellationToken)
    {
        await _inner.RevokeOldestActiveByUserIfLimitIsExceeded(token.UserId, _maxActiveRefreshTokens, cancellationToken);
        await _inner.Add(token, cancellationToken);
    }

    public async Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken)
    {
        return await _inner.GetByToken(token, cancellationToken);
    }

    public async Task Update(RefreshToken tokenEntity, CancellationToken cancellationToken)
    {
        await _inner.Update(tokenEntity, cancellationToken);
    }

    public async Task RevokeOldestActiveByUserIfLimitIsExceeded(Guid userId, int maxAllowed, CancellationToken cancellationToken)
    {
        await _inner.RevokeOldestActiveByUserIfLimitIsExceeded(userId, maxAllowed, cancellationToken);
    }
}