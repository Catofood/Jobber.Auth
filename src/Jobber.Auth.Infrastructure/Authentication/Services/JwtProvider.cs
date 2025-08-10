using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Infrastructure.Authentication.Options;
using Microsoft.Extensions.Options;

namespace Jobber.Auth.Infrastructure.Authentication.Services;

public class JwtProvider(
    IOptionsSnapshot<JwtOptions> jwtOptions, 
    IOptionsSnapshot<PrivateKeyOptions> privateKeyOptions) 
    : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly PrivateKeyOptions _privateKeyOptions = privateKeyOptions.Value;

    public string GenerateToken(Guid userId)
    {
        Claim[] claims = [
            new("userId", userId.ToString()),
        ];
        
        var token = new JwtSecurityToken(
            signingCredentials: _privateKeyOptions.GetSigningCredentials(),
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
            claims: claims);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}   