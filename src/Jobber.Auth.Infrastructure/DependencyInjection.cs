using System.Reflection;
using Jobber.Auth.Application.Interfaces;
using Jobber.Auth.Infrastructure.Persistence;
using Jobber.Auth.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobber.Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        // var thisAssembly = Assembly.GetExecutingAssembly();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<UserDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        return services;
    }
}