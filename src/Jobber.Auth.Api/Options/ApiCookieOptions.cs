namespace Api.Options;

public class ApiCookieOptions
{
    public const string ConfigurationSectionName = "CookieOptions";

    public required string AccessTokenCookieName { get; init; }
    public required string RefreshTokenCookieName { get; init; }
}