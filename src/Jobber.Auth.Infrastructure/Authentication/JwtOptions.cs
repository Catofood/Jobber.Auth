namespace Jobber.Auth.Infrastructure.Authentication;

public class JwtOptions
{
    public const string ConfigurationSectionName = "JwtOptions";
    
    public required string SecretKey { get; init; }
    public required int ExpiresMinutes { get; init; }
}