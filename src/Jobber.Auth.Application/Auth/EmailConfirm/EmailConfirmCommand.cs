using Jobber.Auth.Application.Auth.Dto;
using MediatR;

namespace Jobber.Auth.Application.Auth.EmailConfirm;

public record EmailConfirmCommand : IRequest<Unit>
{
    public required EmailConfirmDto EmailConfirmDto { get; init; }
}