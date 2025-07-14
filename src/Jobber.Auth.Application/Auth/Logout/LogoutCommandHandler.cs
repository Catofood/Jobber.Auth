using Jobber.Auth.Application.Contracts;
using MediatR;

namespace Jobber.Auth.Application.Auth.Logout;

public class LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<Unit> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var refreshTokenEntity = await _refreshTokenRepository.GetByToken(command.RefreshToken, cancellationToken);
        if (refreshTokenEntity is null) return Unit.Value;
        refreshTokenEntity.IsRevoked = false;
        await _refreshTokenRepository.Update(refreshTokenEntity, cancellationToken);
        return Unit.Value;
    }
}