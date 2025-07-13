namespace Jobber.Auth.Infrastructure.Authentication;

public class JwtOptions
{
    public const string ConfigurationSectionName = "JwtOptions";
    
    public string SecretKey { get; set; }
    public int ExpiresMinutes { get; set; }
}