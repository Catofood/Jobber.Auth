using MediatR;

namespace Jobber.Auth.Application.Auth.Commands;

public record RegisterUserCommand : IRequest<Guid>
{
    public string Email { get; set; } 
    public string Password { get; set; }
}
     