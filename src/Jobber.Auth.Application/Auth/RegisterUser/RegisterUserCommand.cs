using Jobber.Auth.Application.Auth.Dto;
using MediatR;

namespace Jobber.Auth.Application.Auth.RegisterUser;

// Returns access jwt token
public record RegisterUserCommand : IRequest<AuthTokensDto>
{
    public required string Email { get; init; } 
    public required string Password { get; init; }
}
     