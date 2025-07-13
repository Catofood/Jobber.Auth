namespace Jobber.Auth.Application.Contracts;

public interface IJwtProvider
{
    public string GenerateToken(Guid userId);
}