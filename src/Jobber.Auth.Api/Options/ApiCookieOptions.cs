namespace Api.Options;

public class ApiCookieOptions
{
    
    public const string ConfigurationSectionName = "CookieOptions"; // Do not rename, Configuration serialization

    public required string AccessTokenCookieName { get; init; } // Do not rename, Configuration serialization
    public required string RefreshTokenCookieName { get; init; } // Do not rename, Configuration serialization
}