using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken);
    Task AddUser(User user, CancellationToken cancellationToken);
}