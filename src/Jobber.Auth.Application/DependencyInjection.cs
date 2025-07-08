using System.Reflection;
using FluentValidation;
using Jobber.Auth.Application.Behaviors;
using Jobber.Auth.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jobber.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(thisAssembly));
        services.AddValidatorsFromAssembly(thisAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAutoMapper(cfg => cfg.AddMaps(thisAssembly));
        return services;
    }
}