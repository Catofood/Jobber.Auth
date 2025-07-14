using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Domain.Entities;
using Jobber.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure.Repositories;

public class RefreshTokenRepository(AuthDbContext dbContext) : IRefreshTokenRepository
{
    private readonly AuthDbContext _dbContext = dbContext;

    public async Task Add(RefreshToken token, CancellationToken cancellationToken)
    {
        await _dbContext.RefreshTokens.AddAsync(token, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken)
    {
        var tokenEntity = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token,cancellationToken: cancellationToken);
        return tokenEntity;
    }
    public async Task RevokeOldestActiveByUserIfLimitIsExceeded(Guid userId, int maxAllowed, CancellationToken cancellationToken)
    {
        var activeTokensCount = await _dbContext
            .RefreshTokens
            .CountAsync(x => x.UserId == userId && x.IsActive, cancellationToken);
        if (activeTokensCount >= maxAllowed)
        {
            var tokensToRevoke = _dbContext
                .RefreshTokens
                .Where(x => x.UserId == userId && x.IsActive)
                .OrderBy(x => x.IssuedAt)
                .Take(activeTokensCount - maxAllowed + 1)
                .ExecuteUpdateAsync(property => 
                    property.SetProperty(x => x.IsRevoked, true), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Update(RefreshToken tokenEntity, CancellationToken cancellationToken)
    {
        _dbContext.RefreshTokens.Update(tokenEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}