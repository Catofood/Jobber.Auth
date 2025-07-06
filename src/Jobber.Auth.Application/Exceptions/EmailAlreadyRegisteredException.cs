namespace Jobber.Auth.Application.Exceptions;

public class EmailAlreadyRegisteredException : Exception
{
    public EmailAlreadyRegisteredException(string email) : base($"Email {email} is already registered") {}
}