using MediatR;

namespace Jobber.Auth.Application.Auth.Logout;

public record LogoutCommand : IRequest<Unit>
{
    public required string RefreshToken { get; init; }
}