using Api.Options;
using Jobber.Auth.Application.Auth.Dto;
using Jobber.Jwt.Options;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class CookieService(
    IOptionsSnapshot<RefreshTokenCookieOptions> refreshTokenOptions,
    IOptionsSnapshot<AccessTokenCookieOptions> accessTokenOptions)
    : ICookieService
{
    private readonly RefreshTokenCookieOptions _refreshTokenOptions = refreshTokenOptions.Value;
    private readonly AccessTokenCookieOptions _accessTokenOptions = accessTokenOptions.Value;

    public void AppendCookieAuthTokens(HttpResponse response, AuthTokensDto authTokensDto)
    {
        var cookies = response.Cookies;
        cookies.Append(_accessTokenOptions.AccessJwtTokenCookieName, authTokensDto.AccessToken);
        cookies.Append(_refreshTokenOptions.RefreshTokenCookieName, authTokensDto.RefreshToken.Token);
    }
}