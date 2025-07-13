using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Domain.Entities;
using Jobber.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _dbContext;

    public UserRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken)
    {
        
        return await _dbContext.Users.AnyAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken: cancellationToken);
    }
    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken: cancellationToken);

    }
    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}