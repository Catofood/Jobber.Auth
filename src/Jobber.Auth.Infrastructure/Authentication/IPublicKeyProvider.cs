using Microsoft.IdentityModel.Tokens;

namespace Jobber.Auth.Infrastructure.Authentication;

public interface IPublicKeyProvider
{
    RsaSecurityKey GetRsaSecurityKey();
}