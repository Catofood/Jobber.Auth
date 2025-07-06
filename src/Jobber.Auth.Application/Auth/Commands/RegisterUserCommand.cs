using MediatR;

namespace Jobber.Auth.Application.Auth.Commands;

public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;