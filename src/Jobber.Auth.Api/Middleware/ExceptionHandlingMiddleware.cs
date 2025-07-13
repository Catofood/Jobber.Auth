using Jobber.Auth.Application.Exceptions;
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
                Status = httpContext.Response.StatusCode,
                Title = ex.Message,
                Type = ex.GetType().Name,
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
            };
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }

    private static int GetApplicationErrorStatusCode(Exception ex) =>
        ex switch
        {
            EmailAlreadyRegisteredException => StatusCodes.Status400BadRequest,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
}