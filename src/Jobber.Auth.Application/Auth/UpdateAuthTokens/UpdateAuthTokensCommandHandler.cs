using Jobber.Auth.Application.Auth.Dto;
using Jobber.Auth.Application.Auth.Services;
using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Application.Exceptions;
using MediatR;

namespace Jobber.Auth.Application.Auth.UpdateAuthTokens;

public class UpdateAuthTokensCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IAuthTokensFacade authTokensFacade)
    : IRequestHandler<UpdateAuthTokensCommand, AuthTokensDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IAuthTokensFacade _authTokensFacade = authTokensFacade;

    public async Task<AuthTokensDto> Handle(UpdateAuthTokensCommand command, CancellationToken cancellationToken)
    {
        var refreshTokenEntity = await _refreshTokenRepository.GetByToken(command.RefreshToken, cancellationToken);
        if (refreshTokenEntity is null) throw new InvalidRefreshTokenException();
        if (refreshTokenEntity.IsRevoked) throw new RevokedRefreshTokenException();
        refreshTokenEntity.IsRevoked = true;
        await _refreshTokenRepository.Update(refreshTokenEntity, cancellationToken);
        var tokens = await _authTokensFacade.CreateAndRegisterTokens(refreshTokenEntity.UserId, cancellationToken);
        return tokens;
    }
}