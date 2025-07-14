namespace Jobber.Auth.Infrastructure.Authentication;

public class JwtOptions
{
    public const string ConfigurationSectionName = "JwtOptions"; // Do not rename, Configuration serialization
    
    public required string SecretKey { get; init; } // Do not rename, Configuration serialization
    public required int ExpiresMinutes { get; init; } // Do not rename, Configuration serialization
}