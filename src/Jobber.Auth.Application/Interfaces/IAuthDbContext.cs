using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Application.Interfaces;

public interface IAuthDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}