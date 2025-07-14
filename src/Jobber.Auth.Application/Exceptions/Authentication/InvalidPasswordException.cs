namespace Jobber.Auth.Application.Exceptions;

public class InvalidPasswordException() : AuthenticationException("Invalid password.") { }