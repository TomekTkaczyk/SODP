using SODP.Domain.Exceptions.ValueObjectExceptions;
using System.Text.RegularExpressions;

namespace SODP.Domain.ValueObjects;

public record ProjectName
{
	public string Value { get; }

    public ProjectName(string name)
    {
		if (!Regex.Match(name, @"^[^\d\s_][\w\s_]*$").Success)
		{
			throw new InvalidProjectNameException(name);
		}

		Value = name.ToUpper();
    }

	public static implicit operator string(ProjectName name) => name?.Value;

	public static implicit operator ProjectName(string name) => new(name.ToUpper());

}
