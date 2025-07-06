using System.Reflection;
using Jobber.Auth.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobber.Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        services.AddScoped<IAuthDbContext, AuthDbContext>();
        services.AddDbContext<AuthDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        return services;
    }
}