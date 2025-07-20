using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication;

public class JwtOptions
{
    public const string ConfigurationSectionName = "JwtOptions"; // Do not rename, Configuration serialization
    
    public required string PrivateKey { get; init; } // Do not rename, Configuration serialization
    public required string PublicKey { get; init; } // Do not rename, Configuration serialization
    public required int ExpiresMinutes { get; init; } // Do not rename, Configuration serialization
    
    public RsaSecurityKey GetPrivateRsaKey()
    {
        if (string.IsNullOrWhiteSpace(PrivateKey))
            throw new InvalidOperationException($"{nameof(JwtOptions)}: {nameof(PrivateKey)} is null or empty.");
        var rsa = RSA.Create();
        rsa.ImportFromPem(PrivateKey);
        var signingKey = new RsaSecurityKey(rsa);
        return signingKey;
    }

    public SigningCredentials GetSigningCredentials()
    {
        return new SigningCredentials(GetPrivateRsaKey(), SecurityAlgorithms.RsaSha256);
    }
    
    public RsaSecurityKey GetPublicRsaKey()
    {
        if (string.IsNullOrWhiteSpace(PublicKey))
            throw new InvalidOperationException($"{nameof(JwtOptions)}: {nameof(PublicKey)} is null or empty.");
        var rsa = RSA.Create();
        rsa.ImportFromPem(PublicKey);
        var signingKey = new RsaSecurityKey(rsa);
        return signingKey;
    }
}