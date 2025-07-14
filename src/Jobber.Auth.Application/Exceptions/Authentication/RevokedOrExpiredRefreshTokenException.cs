namespace Jobber.Auth.Application.Exceptions.Authentication;

public class RevokedOrExpiredRefreshTokenException() : AuthenticationException("Revoked or expired RefreshToken.");