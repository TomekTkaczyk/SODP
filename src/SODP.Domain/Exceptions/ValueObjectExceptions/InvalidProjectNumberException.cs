namespace SODP.Domain.Exceptions.ValueObjectExceptions;

public sealed class InvalidProjectNumberException : DomainException
{
    public InvalidProjectNumberException(string number) : base($"Project number: {number} is invalid.")
    {
        Number = number;
    }

    public string Number { get; }
}
