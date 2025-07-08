using Jobber.Auth.Application.Interfaces;
using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;

    public UserRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellationToken);
    }

    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}