namespace Jobber.Auth.Application.Options;

public class RefreshTokenOptions
{
    public const string ConfigurationSectionName = "RefreshTokenOptions";
    
    public required int ExpiresDays { get; init; }
    
    public required int MaxActiveRefreshTokensPerUser { get; init; }
}