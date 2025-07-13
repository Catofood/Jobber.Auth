using Bogus;
using Jobber.Auth.Application.Contracts;
using Jobber.Auth.Infrastructure;
using Jobber.Auth.Infrastructure.Authentication;
using Xunit.Abstractions;

namespace Jobber.Auth.Tests.Unit.Infrastructure;

public class PasswordHasherTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IPasswordHasher _sut = new BcryptPasswordHasher();

    public PasswordHasherTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void HashPassword_Should_Return_NonEmpty_NonPlainText()
    {
        // Arrange
        var faker = new Faker();
        var password = faker.Internet.Password();
        
        // Act
        var hash = _sut.HashPassword(password);
        
        // Assert
        hash.Should().NotBeNullOrEmpty();
        hash.Should().NotBe(password);
    }

    [Fact]
    public void HashPassword_Should_Return_True_For_Valid_Password()
    {
        // Arrange
        var faker = new Faker();
        var password = faker.Internet.Password();
        var hash = _sut.HashPassword(password);
        
        // Act
        var isValid = _sut.VerifyHashedPassword(hash, password);
        
        // Assert
        isValid.Should().BeTrue();
    }
    
    [Fact]
    public void HashPassword_Should_Return_True_For_Invalid_Password()
    {
        // Arrange
        var faker = new Faker();
        var originalPassword = faker.Internet.Password();
        _testOutputHelper.WriteLine(originalPassword);
        
        var hash = _sut.HashPassword(originalPassword);

        var anotherPassword = faker.Internet.Password();
        _testOutputHelper.WriteLine(anotherPassword);
        
        // Act
        var isValid = _sut.VerifyHashedPassword(hash, anotherPassword);
        
        // Assert
        isValid.Should().BeFalse();
    }
}