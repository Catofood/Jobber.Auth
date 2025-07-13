using System.Runtime.InteropServices;
using Jobber.Auth.Domain.Entities;
using Jobber.Auth.Infrastructure;
using Jobber.Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Jobber.Auth.Tests.Unit.Infrastructure;

public class RefreshTokenRepositoryTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static class ObjectAddressHelper
    {
        public static string GetDebugInfo(object obj)
        {
            var handle = GCHandle.Alloc(obj, GCHandleType.WeakTrackResurrection);
            var address = GCHandle.ToIntPtr(handle);
            var result = $"Hash: {obj.GetHashCode()}, Address: 0x{address.ToString("X")}";
            handle.Free();
            return result;
        }
    }

    [Fact]
    public async Task Update_Should_ChangeDataInDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AuthDbContext(options);
        var repository = new RefreshTokenRepository(dbContext);

        var token1 = "token1";

        var testRefreshToken = new RefreshToken
        {
            UserId = Guid.NewGuid(),
            Token = token1,
            IssuedAt = DateTimeOffset.Now,
            ExpiresAt = DateTimeOffset.Now.AddMinutes(5),
            IsRevoked = false
        };

        _testOutputHelper.WriteLine($"[testRefreshToken] {ObjectAddressHelper.GetDebugInfo(testRefreshToken)}");

        await repository.Add(testRefreshToken, CancellationToken.None);

        // Act
        var refreshTokenEntityFromRepository = await repository.GetByToken(token1, CancellationToken.None);
        _testOutputHelper.WriteLine($"[refreshTokenEntityFromRepository] {ObjectAddressHelper.GetDebugInfo(refreshTokenEntityFromRepository)}");

        refreshTokenEntityFromRepository!.IsRevoked = true;
        await repository.Update(refreshTokenEntityFromRepository, CancellationToken.None);

        var updatedRefreshToken = await repository.GetByToken(token1, CancellationToken.None);
        _testOutputHelper.WriteLine($"[updatedRefreshToken] {ObjectAddressHelper.GetDebugInfo(updatedRefreshToken)}");

        // Assert
        updatedRefreshToken!.IsRevoked.Should().BeTrue();
    }
}
