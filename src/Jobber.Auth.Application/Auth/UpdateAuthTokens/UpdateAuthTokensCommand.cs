using Jobber.Auth.Application.Auth.Dto;
using MediatR;

namespace Jobber.Auth.Application.Auth.UpdateAuthTokens;

// Returns new jwt token
public record UpdateAuthTokensCommand : IRequest<AuthTokensDto>
{
    public required string RefreshToken { get; init; }
}