using System.Text;
using Api.Extensions;
using Api.Options;
using Jobber.Auth.Infrastructure.Authentication;
using Jobber.Auth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetJwtOptions();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[cookieOptions.AccessTokenCookieName];
                        return Task.CompletedTask;
                    }
                };  
            });
        services.AddAuthorization();

        return services;
    }
}