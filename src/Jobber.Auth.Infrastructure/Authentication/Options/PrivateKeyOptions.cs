using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication.Options;

public class PrivateKeyOptions
{
    public required string JwtPrivateKey { get; init; } // Do not rename, Configuration serialization
    
    public RsaSecurityKey GetPrivateRsaKey()
    {
        if (string.IsNullOrWhiteSpace(JwtPrivateKey))
            throw new InvalidOperationException($"{nameof(PrivateKeyOptions)}: {nameof(JwtPrivateKey)} is null or empty.");
        var rsa = RSA.Create();
        rsa.ImportFromPem(JwtPrivateKey);
        var signingKey = new RsaSecurityKey(rsa);
        return signingKey;
    }

    public SigningCredentials GetSigningCredentials()
    {
        return new SigningCredentials(GetPrivateRsaKey(), SecurityAlgorithms.RsaSha256);
    }
}