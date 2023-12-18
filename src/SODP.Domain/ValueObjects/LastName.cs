using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record LastName : ValueObject
{
	public string Value { get; }

	public LastName(string lastName)
	{
		Value = lastName;
	}

	public static LastName Default => new("");


	public static implicit operator string(LastName lastName) => lastName?.Value;

	public static implicit operator LastName(string lastName) => new(lastName);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}