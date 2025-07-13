using Jobber.Auth.Application.Auth.Dto;

namespace Jobber.Auth.Application.Auth.Services;

public interface IAuthTokensFacade
{
    Task<AuthTokensDto> CreateAndRegisterTokens(Guid userId, CancellationToken cancellationToken);
}