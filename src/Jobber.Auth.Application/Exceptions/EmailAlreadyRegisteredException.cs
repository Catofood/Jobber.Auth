namespace Jobber.Auth.Application.Exceptions;

public class EmailAlreadyRegisteredException(string email)
    : ApplicationException($"Email {email} is already registered");