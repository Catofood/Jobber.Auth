using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}