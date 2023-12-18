using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record Address : ValueObject
{
	public string Value { get; }

    public Address(string address)
    {                            
        Value = address ?? "";
    }

    public static Address Default => new("");

	public static implicit operator string(Address address) => address?.Value;

	public static implicit operator Address(string address) => new(address);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
