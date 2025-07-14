namespace Jobber.Auth.Application.Exceptions.Authentication;

public class EmailIsNotRegisteredException(string email)
    : AuthenticationException($"Email {email} is not registered");