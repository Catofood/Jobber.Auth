using Jobber.Auth.Application.Exceptions;
using Jobber.Auth.Application.Exceptions.Authentication;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Jobber.Auth.Application.Exceptions.ApplicationException;
using ValidationException = FluentValidation.ValidationException;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var traceId = httpContext.TraceIdentifier;
        var instance = httpContext.Request.Path;
        
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex) when (ex is ApplicationException || ex is ValidationException)
        {
            _logger.LogWarning(ex, "Handled application exception");
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetApplicationErrorStatusCode(ex);
            var problem = new ProblemDetails
            {
                Type = ex.GetType().Name,
                Title = ex.Message,
                Status = httpContext.Response.StatusCode,
                Extensions = { ["traceId"] = traceId },
                Instance = instance,
            };
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problem = new ProblemDetails
            {
                Title = "An unexpected error occurred",
                Status = 500,
                Type = "ServerError",
                Extensions = { ["traceId"] = traceId },
                Instance = instance,
            };
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }

    private static int GetApplicationErrorStatusCode(Exception ex) =>
        ex switch
        {
            EmailIsNotRegisteredException => StatusCodes.Status401Unauthorized,
            EmailAlreadyRegisteredException => StatusCodes.Status409Conflict,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            ApplicationException => StatusCodes.Status400BadRequest,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
}