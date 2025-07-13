using System.Reflection;
using FluentValidation;
using Jobber.Auth.Application.Auth.Services;
using Jobber.Auth.Application.Behaviors;
using Jobber.Auth.Application.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Jobber.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();

        services.Configure<RefreshTokenOptions>(configuration.GetSection(RefreshTokenOptions.ConfigurationSectionName));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(thisAssembly));
        services.AddValidatorsFromAssembly(thisAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IAuthTokensFacade, AuthTokensFacade>();
        services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
        services.AddAutoMapper(cfg => cfg.AddMaps(thisAssembly));
        return services;
    }
}