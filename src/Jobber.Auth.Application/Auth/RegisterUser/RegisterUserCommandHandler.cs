using Jobber.Auth.Application.Auth.Dto;
using Jobber.Auth.Application.Auth.Services;
using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Application.Exceptions;
using Jobber.Auth.Domain.Entities;
using MediatR;

namespace Jobber.Auth.Application.Auth.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IAuthTokensFacade authTokensFacade)
    : IRequestHandler<RegisterUserCommand, AuthTokensDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IAuthTokensFacade _authTokensFacade = authTokensFacade;

    public async Task<AuthTokensDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsEmailRegistered(command.Email, cancellationToken))
        {
            throw new EmailAlreadyRegisteredException(command.Email);
        }
        var userEntity = new User()
        {
            Email = command.Email.ToLowerInvariant(),
            PasswordHash = _passwordHasher.HashPassword(command.Password),
            IsEmailConfirmed = false,
        };
        await _userRepository.AddUser(userEntity, cancellationToken);
        var tokens = await _authTokensFacade.CreateAndRegisterTokens(userEntity.Id, cancellationToken);
        return tokens;
    }
}