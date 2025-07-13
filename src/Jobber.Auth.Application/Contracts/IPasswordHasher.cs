namespace Jobber.Auth.Application.Contracts;

/// <summary>
/// Defines a contract for securely hashing and verifying user passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Generates a secure hash for the provided plain-text password.
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <returns>A hashed representation of the password, suitable for persistent storage.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies whether the provided plain-text password matches the given hashed password.
    /// </summary>
    /// <param name="hashedPassword">The previously stored hashed password.</param>
    /// <param name="providedPassword">The plain-text password provided for comparison.</param>
    /// <returns><c>true</c> if the password is valid; otherwise, <c>false</c>.</returns>
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}
