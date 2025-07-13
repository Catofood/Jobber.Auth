using MediatR;

namespace Jobber.Auth.Application.Auth.Commands;

public record RegisterUserCommand : IRequest<string>
{
    public string Email { get; set; } 
    public string Password { get; set; }
}
     