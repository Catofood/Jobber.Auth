using Jobber.Auth.Domain.Entities;

namespace Jobber.Auth.Application.Contracts;

public interface IUserRepository
{
    Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task AddUser(User user, CancellationToken cancellationToken);
}