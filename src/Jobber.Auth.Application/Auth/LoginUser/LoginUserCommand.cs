using Jobber.Auth.Application.Auth.Dto;
using MediatR;

namespace Jobber.Auth.Application.Auth.LoginUser;

// Returns access jwt token
public record LoginUserCommand : IRequest<AuthTokensDto>
{
    public string Email { get; set; } 
    public string Password { get; set; }
}
