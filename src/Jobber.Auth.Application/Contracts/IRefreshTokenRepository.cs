using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Contracts;

public interface IRefreshTokenRepository
{
    Task Add(RefreshToken token, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken);
    Task Update(RefreshToken tokenEntity, CancellationToken cancellationToken);
    Task RevokeOldestActiveByUserIfLimitIsExceeded(Guid userId, int maxAllowed, CancellationToken cancellationToken);
}