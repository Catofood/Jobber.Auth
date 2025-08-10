using Jobber.Auth.Application.Contracts;
using MediatR;

namespace Jobber.Auth.Application.Auth.EmailConfirm;

public class EmailConfirmHandler : IRequestHandler<EmailConfirmCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public EmailConfirmHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(EmailConfirmCommand command, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }
}