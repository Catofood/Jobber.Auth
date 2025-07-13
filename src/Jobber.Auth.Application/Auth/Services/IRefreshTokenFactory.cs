using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Auth.Services;

public interface IRefreshTokenFactory
{
    RefreshToken Create(Guid userId);
}