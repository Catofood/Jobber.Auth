using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jobber.Auth.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication;

public class JwtProvider(IOptionsMonitor<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.CurrentValue;

    public string GenerateToken(Guid userId)
    {
        Claim[] claims = [
            new("userId", userId.ToString()),
        ];
        
        var token = new JwtSecurityToken(
            signingCredentials: _jwtOptions.GetSigningCredentials(),
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
            claims: claims);
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}