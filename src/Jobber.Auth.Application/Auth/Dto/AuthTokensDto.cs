using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Auth.Dto;

public record AuthTokensDto
{
    public required RefreshToken RefreshToken { get; init; }
    public required string AccessToken { get; init; }
}
