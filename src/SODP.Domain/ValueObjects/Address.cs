namespace SODP.Domain.ValueObjects;

public record Address
{
	public string Value { get; }

    public Address(string address)
    {                            
        Value = address;
    }

	public static implicit operator string(Address address) => address?.Value;

	public static implicit operator Address(string address) => new(address);

}
