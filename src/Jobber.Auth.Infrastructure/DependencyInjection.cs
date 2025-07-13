using System.Reflection;
using System.Text;
using Jobber.Auth.Application.Interfaces;
using Jobber.Auth.Infrastructure.Authentication;
using Jobber.Auth.Infrastructure.Persistence;
using Jobber.Auth.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.ConfigurationSectionName));

        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<UserDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        return services;
    }
}