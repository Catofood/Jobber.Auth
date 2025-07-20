using Api.Extensions;
using Api.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var publicKeyOptions = configuration.GetJwtPublicKeyOptions();
        services.Configure<ApiCookieOptions>(configuration.GetSection(ApiCookieOptions.ConfigurationSectionName));
        var cookieOptions = configuration.GetCookieOptions();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = publicKeyOptions.GetRsaSecurityKey()
                };

                options.Events = new JwtBearerEvents
                {
                    
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
                        if (!string.IsNullOrEmpty(authHeader) &&
                            authHeader.StartsWith(JwtBearerDefaults.AuthenticationScheme + " "))
                        {
                            context.Token = authHeader[(JwtBearerDefaults.AuthenticationScheme.Length + 1)..];
                        }
                        else
                        {
                            context.Token = context.Request.Cookies[cookieOptions.AccessTokenCookieName];
                        }
                        return Task.CompletedTask;
                    }
                };  
            });
        services.AddAuthorization();
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}