using Jobber.Auth.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Options;

public class ConfigureJwtBearerOptions(
    IPublicKeyProvider publicKeyProvider,
    IOptionsMonitor<ApiCookieOptions> apiCookieOptions)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options) => Configure(JwtBearerDefaults.AuthenticationScheme, options);

    public void Configure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver =
                ((token, securityToken, kid, parameters) => [publicKeyProvider.GetRsaSecurityKey()])
        };
        options.Events = new JwtBearerEvents
        {
                    
            OnMessageReceived = context =>
            {
                var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) &&
                    authHeader.StartsWith(JwtBearerDefaults.AuthenticationScheme + " "))
                {
                    context.Token = authHeader[(JwtBearerDefaults.AuthenticationScheme.Length + 1)..];
                }
                else
                {
                    context.Token = context.Request.Cookies[apiCookieOptions.CurrentValue.AccessTokenCookieName];
                }
                return Task.CompletedTask;
            }
        };  
    }
}