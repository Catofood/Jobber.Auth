using Jobber.Auth.Application.Auth.Dto;
using MediatR;

namespace Jobber.Auth.Application.Auth.RegisterUser;

// Returns access jwt token
public record RegisterUserCommand : IRequest<AuthTokensDto>
{
    public string Email { get; set; } 
    public string Password { get; set; }
}
     