using Api.Options;
using Jobber.Auth.Application.Auth.Dto;

namespace Api.Extensions;

public static class HttpResponseExtensions
{
    public static HttpResponse AppendCookieAuthTokens(this HttpResponse response, ApiCookieOptions options, AuthTokensDto authTokensDto)
    {
        var cookies = response.Cookies;
        cookies.Append(options.AccessTokenCookieName, authTokensDto.AccessToken);
        cookies.Append(options.RefreshTokenCookieName, authTokensDto.RefreshToken.Token);
        return response;
    }
    
}