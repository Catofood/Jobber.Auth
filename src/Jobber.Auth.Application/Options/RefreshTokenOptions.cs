namespace Jobber.Auth.Application.Options;

public class RefreshTokenOptions
{
    public const string ConfigurationSectionName = "RefreshTokenOptions"; // Do not rename, Configuration serialization
    
    public required int ExpiresDays { get; init; } // Do not rename, Configuration serialization
    
    public required int MaxActiveRefreshTokensPerUser { get; init; } // Do not rename, Configuration serialization
}