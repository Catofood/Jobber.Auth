using Jobber.Auth.Domain.Entities;
using Jobber.Auth.Infrastructure;
using Jobber.Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Jobber.Auth.Tests.Unit.Infrastructure;

public class UserRepositoryTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UserRepositoryTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task AddUser_ShouldAssignNonEmptyGuidId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // уникальность между тестами
            .Options;

        await using var dbContext = new AuthDbContext(options);
        var repository = new UserRepository(dbContext);

        var user = new User
        {
            Email = "test@example.com",
            PasswordHash = "example",
            IsEmailConfirmed = false,
        };
        _testOutputHelper.WriteLine(user.Id.ToString());
        // Act
        await repository.Add(user, CancellationToken.None);

        // Assert
        _testOutputHelper.WriteLine(user.Id.ToString());
        Assert.NotEqual(Guid.Empty, user.Id); // <-- Проверка
    }


}