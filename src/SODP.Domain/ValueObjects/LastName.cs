namespace SODP.Domain.ValueObjects;

public record LastName
{
	public string Value { get; }

	public LastName(string lastName)
	{
		Value = lastName;
	}

	public static implicit operator string(LastName lastName) => lastName?.Value;

	public static implicit operator LastName(string lastName) => new(lastName);
}