using Api;
using Api.Middleware;
using Jobber.Auth.Application;
using Jobber.Auth.Infrastructure;
using Jobber.Auth.Infrastructure.Authentication;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.MapOpenApi();
    app.UseCors("AllowFrontend");
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();    