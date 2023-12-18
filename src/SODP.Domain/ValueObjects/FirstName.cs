using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record FirstName	: ValueObject
{
	public string Value { get; }

	public FirstName(string firstName)
	{
		Value = firstName ?? "" ;
	}

	public static FirstName Default => new("");

	public static implicit operator string(FirstName firstName) => firstName?.Value;

	public static implicit operator FirstName(string firstName) => new(firstName);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
