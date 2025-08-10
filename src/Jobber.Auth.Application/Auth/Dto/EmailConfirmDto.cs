namespace Jobber.Auth.Application.Auth.Dto;

public record EmailConfirmDto
{
    public required Guid UserId { get; init; }
    public required string Token { get; init; }
}