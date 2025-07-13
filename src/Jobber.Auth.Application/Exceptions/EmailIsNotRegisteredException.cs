namespace Jobber.Auth.Application.Exceptions;

public class EmailIsNotRegisteredException(string email)
    : ApplicationException($"Email {email} is not registered");