namespace Api.Options;

public class ApiCookieOptions
{
    public const string ConfigurationSectionName = "CookieOptions";

    public string AccessTokenCookieName { get; init; } = "access_token";
    public string RefreshTokenCookieName { get; init; } = "refresh_token";
}