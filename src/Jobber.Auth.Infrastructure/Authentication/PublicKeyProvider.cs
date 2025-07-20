using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication;

public class PublicKeyProvider : IPublicKeyProvider
{
    private readonly IOptionsMonitor<PublicKeyOptions> _options;

    public PublicKeyProvider(IOptionsMonitor<PublicKeyOptions> options)
    {
        _options = options;
    }

    public RsaSecurityKey GetRsaSecurityKey()
    {
        var key = _options.CurrentValue.JwtPublicKey;
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException($"{nameof(PublicKeyOptions)}: {nameof(key)} is null or empty.");
        using var rsa = RSA.Create();
        rsa.ImportFromPem(key);
        var signingKey = new RsaSecurityKey(rsa.ExportParameters(false));
        return signingKey;
    }
}