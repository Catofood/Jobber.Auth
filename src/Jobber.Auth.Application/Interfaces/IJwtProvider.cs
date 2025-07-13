using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(Guid userId);
}