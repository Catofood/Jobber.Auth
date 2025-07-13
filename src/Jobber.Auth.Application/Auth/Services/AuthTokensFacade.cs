using Jobber.Auth.Application.Auth.Dto;
using Jobber.Auth.Application.Contracts;

namespace Jobber.Auth.Application.Auth.Services;

public class AuthTokensFacade(
    IRefreshTokenFactory refreshTokenFactory,
    IJwtProvider jwtProvider,
    IRefreshTokenRepository refreshTokenRepository) 
    : IAuthTokensFacade
{
    private readonly IRefreshTokenFactory _refreshTokenFactory = refreshTokenFactory;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<AuthTokensDto> CreateAndRegisterTokens(Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = _refreshTokenFactory.Create(userId);
        var accessToken = _jwtProvider.GenerateToken(userId);
        await _refreshTokenRepository.Add(refreshToken, cancellationToken);
        var tokens = new AuthTokensDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
        return tokens;
    }
}