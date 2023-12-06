namespace SODP.Domain.ValueObjects;

public record FirstName
{
	public string Value { get; }

	public FirstName(string firstName)
	{
		Value = firstName;
	}

	public static implicit operator string(FirstName firstName) => firstName?.Value;

	public static implicit operator FirstName(string firstName) => new(firstName);
}
