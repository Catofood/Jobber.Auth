namespace Jobber.Auth.Application.Exceptions;

public class EmailIsNotRegisteredException(string email)
    : AuthenticationException($"Email {email} is not registered");