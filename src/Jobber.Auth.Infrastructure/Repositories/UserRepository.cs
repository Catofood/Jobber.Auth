using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = _dbContext.Set<User>();
    }
    
    public async Task<bool> IsEmailRegistered(string email, CancellationToken cancellationToken)
    {
        
        return await _users.AnyAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken: cancellationToken);
    }
    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _users.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken: cancellationToken);

    }

    public async Task Add(User user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task<string?> GetEmailConfirmationTokenByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await _users
            .Where(u => u.Id == userId)
            .Select(u => u.EmailConfirmationToken)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(User user)
    {
        _users.Update(user);
    }

    public async Task<User?> GetById(Guid userId, CancellationToken cancellationToken)
    {
        return await _users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}