using AutoMapper;
using Jobber.Auth.Application.Exceptions;
using Jobber.Auth.Application.Interfaces;
using Jobber.Auth.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Application.Auth.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsEmailRegistered(command.Email, cancellationToken))
        {
            throw new EmailAlreadyRegisteredException(command.Email);
        }
        var newUserEntity = new User()
        {
            Email = command.Email.ToLowerInvariant(),
            PasswordHash = _passwordHasher.HashPassword(command.Password),
            IsEmailConfirmed = false,
        };
        await _userRepository.AddUser(newUserEntity, cancellationToken);
        // TODO: Добавить реализацию подтверждения создания аккаунта с помощью почты
        // Либо чтобы нужно было перейти по ссылке, либо чтобы нужно было ввести код в течение n часов
        // Нужно использовать для этого SMTP клиент
        // Мб добавить ещё капчу
        return newUserEntity.Id;
    }
}