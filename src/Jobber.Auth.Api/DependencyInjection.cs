using Api.Options;
using Api.Services;
using Jobber.Auth.Infrastructure.Authentication;
using Jobber.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<ICookieService, CookieService>();
        services.Configure<RefreshTokenCookieOptions>(configuration);
        services.AddJobberJwt(configuration);
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddControllers().AddDataAnnotationsLocalization();
        services.AddOpenApi();

        return services;
    }
}