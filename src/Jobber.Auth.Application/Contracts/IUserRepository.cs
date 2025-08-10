using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Contracts;

public interface IUserRepository : IUnitOfWork
{
    Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<string?> GetEmailConfirmationTokenByUserId(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetById(Guid userId, CancellationToken cancellationToken);
    void Update(User user);
    Task Add(User user, CancellationToken cancellationToken);
}