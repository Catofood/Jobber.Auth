using System.Text;
using Api.Options;
using Jobber.Auth.Infrastructure.Authentication;

namespace Api.Extensions;

public static class ConfigurationExtensions
{
    public static PublicKeyOptions GetJwtPublicKeyOptions(this IConfiguration configuration)
    {
        var options = configuration
            .Get<PublicKeyOptions>();

        if (options is null)
            throw new InvalidOperationException($"{nameof(PublicKeyOptions)}' is missing or invalid.");

        return options;
    }
    
    public static JwtOptions GetJwtOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(JwtOptions.ConfigurationSectionName);
        var options = section.Get<JwtOptions>();

        if (options is null)
            throw new InvalidOperationException($"Section '{JwtOptions.ConfigurationSectionName}' is missing or invalid.");

        return options;
    }
    
    public static ApiCookieOptions GetCookieOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(ApiCookieOptions.ConfigurationSectionName);
        var options = section.Get<ApiCookieOptions>();

        if (options is null)
            throw new InvalidOperationException($"Section '{ApiCookieOptions.ConfigurationSectionName}' is missing or invalid.");
        return options;
    }
}