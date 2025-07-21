using Jobber.Auth.Application.Auth.Dto;

namespace Api.Services;

public interface ICookieService
{
    void AppendCookieAuthTokens(HttpResponse response, AuthTokensDto authTokensDto);
}