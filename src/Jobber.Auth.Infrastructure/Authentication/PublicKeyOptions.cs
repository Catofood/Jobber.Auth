using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication;

public class PublicKeyOptions
{
    public required string JwtPublicKey { get; init; } // Do not rename, Configuration serialization
    
    public RsaSecurityKey GetRsaSecurityKey()
    {
        if (string.IsNullOrWhiteSpace(JwtPublicKey))
            throw new InvalidOperationException($"{nameof(PublicKeyOptions)}: {nameof(JwtPublicKey)} is null or empty.");
        var rsa = RSA.Create();
        rsa.ImportFromPem(JwtPublicKey);
        var signingKey = new RsaSecurityKey(rsa);
        return signingKey;
    }
}