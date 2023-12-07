using SODP.Domain.Exceptions.ValueObjectExceptions;
using System;
using System.Text.RegularExpressions;

namespace SODP.Domain.ValueObjects;

public record ProjectName
{
	public string Value { get; }

    public ProjectName(string name)
    {
		if (name == null) throw new ArgumentNullException("name");

		Value = Regex.Replace(
			string.Join("_", name.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)), 
			@"_+", @"_");

		if (!Regex.Match(Value, @"^[a-zA-Z][\w\s_]*[a-zA-Z\d]$").Success)
		{
			throw new InvalidProjectNameException(name);
		}
	}

	public static implicit operator string(ProjectName name) => name?.Value;

	public static implicit operator ProjectName(string name) => new(name);
}
