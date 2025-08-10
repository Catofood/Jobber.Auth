using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Infrastructure.Authentication;
using Jobber.Auth.Infrastructure.Authentication.Options;
using Jobber.Auth.Infrastructure.Authentication.Services;
using Jobber.Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Jobber.Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<PrivateKeyOptions>(configuration);
        services.Configure<JwtOptions>(configuration);
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.Decorate<IRefreshTokenRepository, RefreshTokenRepositoryDecorator>();
        services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddDbContext<AuthDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        return services;
    }
}