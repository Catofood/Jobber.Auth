namespace Jobber.Auth.Application.Exceptions.Authentication;

public class InvalidPasswordException() : AuthenticationException("Invalid password.") { }