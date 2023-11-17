namespace SODP.Domain.Exceptions.ValueObjectExceptions;

public class InvalidProjectNameException : DomainException
{
    public InvalidProjectNameException(string name) : base($"Project name: {name} is invalid. ")
    {
        ProjectName = name;
    }

    public string ProjectName { get; }
}
