using Jobber.Auth.Application.Interfaces;
using Jobber.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobber.Auth.Infrastructure;

public class AuthDbContext : DbContext, IAuthDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}