using Jobber.Auth.Application.Auth.Dto;
using Jobber.Auth.Application.Auth.Services;
using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Application.Exceptions;
using Jobber.Auth.Application.Exceptions.Authentication;
using MediatR;

namespace Jobber.Auth.Application.Auth.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IAuthTokensFacade authTokensFacade): IRequestHandler<LoginUserCommand, AuthTokensDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IAuthTokensFacade _authTokensFacade = 
        authTokensFacade;

    public async Task<AuthTokensDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByEmail(command.Email, cancellationToken);
        if (userEntity is null) throw new EmailIsNotRegisteredException(command.Email);
        var userPasswordHash = userEntity.PasswordHash;
        var isPasswordCorrect = _passwordHasher.VerifyHashedPassword(userPasswordHash, command.Password);
        if (!isPasswordCorrect) throw new InvalidPasswordException();
        
        var tokens = await _authTokensFacade.CreateAndRegisterTokens(userEntity.Id, cancellationToken);
        return tokens;
    }
}