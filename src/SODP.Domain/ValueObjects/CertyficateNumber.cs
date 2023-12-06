namespace SODP.Domain.ValueObjects;

public record CertyficateNumber
{
	public string Value { get; }

	public CertyficateNumber(string number)
	{
		Value = number;
	}

	public static implicit operator string(CertyficateNumber number) => number?.Value;

	public static implicit operator CertyficateNumber(string number) => new(number);
}
