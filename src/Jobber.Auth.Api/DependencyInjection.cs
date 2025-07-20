using Api.Extensions;
using Api.Options;
using Jobber.Auth.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var cookieOptions = configuration.GetCookieOptions();
        services.Configure<ApiCookieOptions>(configuration.GetSection(ApiCookieOptions.ConfigurationSectionName));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        services.AddAuthorization();
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}