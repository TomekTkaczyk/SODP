using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Exceptions.ValueObjectExceptions;
using System.Text.RegularExpressions;

namespace SODP.Domain.ValueObjects;

public record ProjectNumber
{
	public string Value { get; }

    public ProjectNumber(string number)
    {
		if (!Regex.Match(number, @"^\d{2}(?!00)\d{2}$").Success)
		{
			throw new InvalidProjectNumberException(number);
		}
		Value = number;
	}

	public static implicit operator string(ProjectNumber number) => number?.Value;

	public static implicit operator ProjectNumber(string number) => new(number);
}
